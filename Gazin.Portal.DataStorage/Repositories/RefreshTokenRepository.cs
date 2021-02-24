using Gazin.Portal.Data.Entities;
using MeControla.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Gazin.Portal.DataStorage.Repositories
{
    public class RefreshTokenRepository : BaseAsyncRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IDbAppContext context)
            : base(context, context.RefreshTokens)
        { }

        public async Task<RefreshToken> FindByTokenAsync(Guid refreshToken)
            => await dbSet.SingleOrDefaultAsync(itm => itm.Uuid.Equals(refreshToken));
    }
}