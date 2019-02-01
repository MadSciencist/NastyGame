namespace Api.Identity.Infrastructure
{
    public interface IPasswordHasher
    {
        string CreateHashString(string passwordToHash);
        bool IsPasswordMatching(string savedPasswordHash, string plainPasswordToCompare);
    }
}