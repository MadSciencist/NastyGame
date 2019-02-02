namespace Api.Identity.Services
{
    public class UserAuthenticator : IUserAuthenticator
    {
        private readonly IPasswordHasher _passwordHasher;

        public UserAuthenticator(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public bool Authenticate(string password, string hashedPassword)
        {
            return _passwordHasher.IsPasswordMatching(hashedPassword, password);
        }
    }
}
