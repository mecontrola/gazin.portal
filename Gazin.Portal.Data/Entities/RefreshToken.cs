using MeControla.Core.Data.Entities;
using System;

namespace Gazin.Portal.Data.Entities
{
    public class RefreshToken : IEntity
    {
        public long Id { get; set; }
        public Guid Uuid { get; set; }
        public string Token { get; set; }
        public DateTime Expired { get; set; }
        public DateTime Created { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}