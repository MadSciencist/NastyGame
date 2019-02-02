namespace Api.Identity.Services
{
    public interface IPasswordHasher
    {
        string CreateHashString(string passwordToHash);
        bool IsPasswordMatching(string savedPasswordHash, string plainPasswordToCompare);
    }
}