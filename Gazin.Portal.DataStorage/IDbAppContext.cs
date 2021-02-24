using Gazin.Portal.Data.Entities;
using MeControla.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gazin.Portal.DataStorage
{
    public interface IDbAppContext : IDbContext
    {
        DbSet<Holiday> Holidays { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<WorkdayOfWeek> WorkdayOfWeeks { get; }
    }
}