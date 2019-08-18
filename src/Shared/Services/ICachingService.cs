namespace Shared.Services
{
    public interface ICachingService
    {
        string Get(string key);
        void Set(string key, string value);
    }
}