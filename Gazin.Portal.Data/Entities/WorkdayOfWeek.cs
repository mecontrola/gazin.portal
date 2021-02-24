using MeControla.Core.Data.Entities;
using System;

namespace Gazin.Portal.Data.Entities
{
    public class WorkdayOfWeek : IEntity
    {
        public long Id { get; set; }
        public Guid Uuid { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool Active { get; set; }
    }
}