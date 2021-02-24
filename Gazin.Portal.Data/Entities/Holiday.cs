using MeControla.Core.Data.Entities;
using System;

namespace Gazin.Portal.Data.Entities
{
    public class Holiday : IEntity
    {
        public long Id { get; set; }
        public Guid Uuid { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool Active { get; set; }
    }
}