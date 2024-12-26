using Ballast.Data.Entities.Identity;
using Ballast.Data.Stores;

namespace Ballast.Service.Identity
{
    public class UserManager : IDisposable
    {
        private readonly UserStore _userStore;

        public UserManager()
        {
            _userStore = new UserStore();
        }

        public User? FindByUsername(string username)
        {
            username = username.Trim().ToLower();
            return _userStore.Get(c => c.Username.ToLower() == username);
        }

        public User RegisterUser(string username, string password, string firstName, string lastName)
        {
            if(FindByUsername(username) != null)
            {
                throw new Exception("User already exists");
            }
            PasswordHasher.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            var user = new User
            {
                Username = username,
                Password = passwordHash,
                SecurityStamp = passwordSalt,
                RegistrationDate = DateTime.Now
            };
            _userStore.RegisterUser(user);
            return user;
        }

        public User VerifyUserLogin(string username, string password)
        {
            var user = FindByUsername(username);
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }
            var verification = PasswordHasher.VerifyPasswordHash(password, user.Password, user.SecurityStamp);
            if (!verification)
            {
                throw new Exception("Invalid user credentials.");
            }
            return user;
        }

        public void Dispose()
        {
            _userStore.Dispose();
        }
    }
}
