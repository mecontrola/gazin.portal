using Gazin.Portal.Core.Services;
using Gazin.Portal.Data.Dtos;
using Gazin.Portal.Data.Dtos.Inputs;
using Gazin.Portal.Integrations.Jira;
using Gazin.Portal.Integrations.Jira.Data.Dtos.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Core.Business
{
    public class ReportBusiness : IReportBusiness
    {
        private readonly IIssueWorklogGetAll issueWorklogGetAll;
        private readonly ISearchPost searchPost;
        private readonly IWorkdayOfWeekService workdayOfWeekService;

        public ReportBusiness(IIssueWorklogGetAll issueWorklogGetAll,
                              ISearchPost searchPost,
                              IWorkdayOfWeekService workdayOfWeekService)
        {
            this.issueWorklogGetAll = issueWorklogGetAll;
            this.searchPost = searchPost;
            this.workdayOfWeekService = workdayOfWeekService;
        }

        public async Task<IList<WorklogReportDto>> ReportPeriod(WorklogReportInputDto input, CancellationToken cancellationToken)
        {
            var request = CreateRequest(input);

            var response = await searchPost.Execute(input.Username, input.Password, request, cancellationToken);

            var range = await workdayOfWeekService.GetAvailableDays(input.StartDate, input.EndDate);
            var list = new List<Swap>();

            foreach (var issue in response.Issues)
                list.AddRange(await GenerateWorklogReportDto(input, issue.Key, cancellationToken));

            return list.Where(itm => range.Any(date => itm.Date.Equals(date.Date)))
                       .OrderBy(itm => itm.Date)
                       .GroupBy(itm => itm.Date)
                       .Select(itm =>
                       {
                           var total = itm.Sum(val => val.Hours);
                           return new WorklogReportDto
                           {
                               Date = itm.Key.ToString("dd/MM/yyyy"),
                               TotalHours = TimeSpan.FromSeconds(total).ToString(@"hh\:mm"),
                               Worklogs = itm.Select(val => new WorlogIssueReportDto
                               {
                                   Issue = val.Issue,
                                   Worklog = val.Worklog,
                                   Hours = TimeSpan.FromSeconds(val.Hours).ToString(@"hh\:mm")
                               }).ToList()
                           };
                       }).ToList();
        }

        private static SearchInputDto CreateRequest(WorklogReportInputDto input)
        {
            return new SearchInputDto
            {
                Jql = $"worklogAuthor in ('{string.Join("','", input.Accounts)}') and worklogDate >= {input.StartDate:yyyy-MM-dd} and worklogDate <= {input.EndDate:yyyy-MM-dd}",
                StartAt = 0,
                MaxResults = 100,
                Fields = new[] { "names" }
            };
        }

        private async Task<IList<Swap>> GenerateWorklogReportDto(WorklogReportInputDto input,
                                                                      string issue,
                                                                      CancellationToken cancellationToken)
        {
            var worklogs = await issueWorklogGetAll.Execute(input.Username, input.Password, issue, cancellationToken);
            return worklogs.Worklogs.Select(itm => new Swap
            {
                Issue = issue,
                Worklog = itm.Id,
                Date = DateTime.Parse(itm.Started.Insert(itm.Started.Length - 2, ":")).Date,
                Hours = itm.TimeSpentSeconds
            }).ToList();
        }

        class Swap {
            public string Issue { get; set; }
            public string Worklog { get; set; }
            public DateTime Date { get; set; }
            public long Hours { get; set; }
        }
    }
}