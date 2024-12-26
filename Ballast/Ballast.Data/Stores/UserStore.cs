using Ballast.Data.Entities.Bookshop;
using Ballast.Data.Entities.Identity;
using LiteDB;
using System.Linq.Expressions;

namespace Ballast.Data.Stores
{
    public class UserStore : IDisposable
    {
        private readonly LiteDatabase _database;
        public UserStore()
        {
            _database = new LiteDatabase(@"ballast-booktstore.db");
        }

        private ILiteCollection<User> Users()
        {
            return _database.GetCollection<User>("Users");
        }

        public User RegisterUser(User user)
        {
            Users().Insert(user);
            return user;
        }

        public User UpdateUser(User user)
        {
            Users().Update(user);
            return user;
        }

        public User Get(Expression<Func<User, bool>> query)
        {
            return Users().FindOne(query);
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
