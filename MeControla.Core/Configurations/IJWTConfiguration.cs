using System;

namespace MeControla.Core.Configurations
{
    public interface IJWTConfiguration
    {
        string Secret { get; }
        string Issuer { get; }
        string Audience { get; }
        TimeSpan TimeToExpire { get; }
        bool Enabled { get; }
    }
}