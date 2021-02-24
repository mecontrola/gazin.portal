namespace MeControla.Core.Configurations
{
    public interface ICorsConfiguration
    {
        string[] Origins { get; }
        bool Enabled { get; }
    }
}