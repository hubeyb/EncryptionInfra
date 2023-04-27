namespace EncryptionInfra.Domain.Interfaces
{
    public interface ICache
    {
        T Get<T>(string key);

        bool Set<T>(string key, T value, TimeSpan expiryTime);

        object Remove(string key);
    }
}