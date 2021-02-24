using Gazin.Portal.Data.Dtos;
using Gazin.Portal.Data.Dtos.Inputs;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Core.Business
{
    public interface IAuthorizationBusiness
    {
        Task<AuthorizationDto> Login(CredentialInputDto credentials, CancellationToken cancellationToken);
    }
}