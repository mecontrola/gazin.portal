using System;

namespace Gazin.Portal.Data.Dtos.Inputs
{
    public class RefreshTokenInputDto
    {
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
    }
}