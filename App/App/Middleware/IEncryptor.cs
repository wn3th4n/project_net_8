namespace IdentityAPI.Middleware
{
    public interface IEncryptor
    {
        string GetSalt();
        string GetHash(string value, string salt);
    }
}
