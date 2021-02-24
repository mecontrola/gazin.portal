using Gazin.Portal.Data.Entities;
using MeControla.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Gazin.Portal.DataStorage.Repositories
{
    public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken>
    {
        Task<RefreshToken> FindByTokenAsync(Guid refreshToken);
    }
}