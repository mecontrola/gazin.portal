using Gazin.Portal.Data.Dtos;
using Gazin.Portal.Data.Dtos.Inputs;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Core.Business
{
    public interface IIssueWorklogBusiness
    {
        Task<IssueWorklogDto> Get(IssueWorklogInputDto input, CancellationToken cancellationToken);
        Task<IssueWorklogDto> InsertWorklog(IssueWorklogInputDto input, CancellationToken cancellationToken);
        Task<bool> RemoveWorklog(IssueWorklogInputDto input, CancellationToken cancellationToken);
        Task<IssueWorklogDto> UpdateWorklog(IssueWorklogInputDto input, CancellationToken cancellationToken);
    }
}