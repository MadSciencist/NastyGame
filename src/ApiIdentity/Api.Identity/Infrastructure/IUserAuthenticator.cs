namespace Api.Identity.Infrastructure
{
    public interface IUserAuthenticator
    {
        bool Authenticate(string password, string hashedPassword);
    }
}