using Gazin.Portal.Core.Exceptions;
using Gazin.Portal.Data.Dtos;
using Gazin.Portal.Data.Dtos.Inputs;
using Gazin.Portal.Integrations.Jira;
using Gazin.Portal.Integrations.Jira.Data.Dtos;
using Gazin.Portal.Integrations.Jira.Data.Dtos.Inputs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Core.Business
{
    public class IssueWorklogBusiness : IIssueWorklogBusiness
    {
        private readonly IIssueWorklogGet issueWorklogGet;
        private readonly IIssueWorklogPost issueWorklogPost;
        private readonly IIssueWorklogPut issueWorklogPut;
        private readonly IIssueWorklogDelete issueWorklogDelete;

        public IssueWorklogBusiness(IIssueWorklogGet issueWorklogGet,
                                    IIssueWorklogPost issueWorklogPost,
                                    IIssueWorklogPut issueWorklogPut,
                                    IIssueWorklogDelete issueWorklogDelete)
        {
            this.issueWorklogGet = issueWorklogGet;
            this.issueWorklogPost = issueWorklogPost;
            this.issueWorklogPut = issueWorklogPut;
            this.issueWorklogDelete = issueWorklogDelete;
        }

        public async Task<IssueWorklogDto> Get(IssueWorklogInputDto input, CancellationToken cancellationToken)
        {
            var response = await issueWorklogGet.Execute(input.Username,
                                                         input.Password,
                                                         input.Issue,
                                                         input.Worklog,
                                                         cancellationToken);

            return CreateResponse(response);
        }

        public async Task<IssueWorklogDto> InsertWorklog(IssueWorklogInputDto input, CancellationToken cancellationToken)
        {
            var worklog = CreateRequest(input);

            var response = await issueWorklogPost.Execute(input.Username,
                                                          input.Password,
                                                          input.Issue,
                                                          worklog,
                                                          cancellationToken);

            return CreateResponse(response);
        }

        public async Task<IssueWorklogDto> UpdateWorklog(IssueWorklogInputDto input, CancellationToken cancellationToken)
        {
            var worklog = CreateRequest(input);

            var response = await issueWorklogPut.Execute(input.Username,
                                                         input.Password,
                                                         input.Issue,
                                                         input.Worklog,
                                                         worklog,
                                                         cancellationToken);

            return CreateResponse(response);
        }

        public async Task<bool> RemoveWorklog(IssueWorklogInputDto input, CancellationToken cancellationToken)
            => await issueWorklogDelete.Execute(input.Username,
                                                input.Password,
                                                input.Issue,
                                                input.Worklog,
                                                cancellationToken);

        private static IssueWorklogDto CreateResponse(WorklogDto response)
            => new IssueWorklogDto
            {
                Id = response.Id,
                IssueId = response.IssueId
            };

        private static WorklogInputDto CreateRequest(IssueWorklogInputDto input)
            => new WorklogInputDto
            {
                Started = MountDateTimeStarted(input.Date, input.StartTime),
                TimeSpentSeconds = MountTimeSpentSeconds(input.StartTime, input.EndTime),
                Comment = new DocumentFormat
                {
                    Type = "doc",
                    Version = 1,
                    Content = new List<DocumentFormatContent>
                    {
                        new DocumentFormatContent
                        {
                            Type = "paragraph",
                            Content = new List<DocumentFormatContentValue>
                            {
                                new DocumentFormatContentValue { Text = SatinizeString(input.Comment), Type = "text" }
                            }

                        }

                    }
                }
            };

        private static string MountDateTimeStarted(DateTime date, TimeSpan time)
        {
            var rtn = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0);
            return $"{rtn:yyyy-MM-ddTHH:mm:ss.fff}{rtn.ToString("zzz").Replace(":", string.Empty)}";
        }

        private static long MountTimeSpentSeconds(TimeSpan startTime, TimeSpan endTime)
            => startTime > endTime
             ? throw new TimeSpanRangeException(nameof(startTime), nameof(endTime))
             : (long)endTime.Subtract(startTime).TotalSeconds;

        private static string SatinizeString(string value)
            => string.IsNullOrWhiteSpace(value)
             ? null
             : value;
    }
}