﻿using System;

namespace Gazin.Portal.Data.Dtos
{
    public class AuthorizationDto
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public Guid RefreshToken { get; set; }
        public DateTime? Expiration { get; set; }
    }
}