namespace Api.Identity.Services
{
    public interface IUserAuthenticator
    {
        bool Authenticate(string password, string hashedPassword);
    }
}