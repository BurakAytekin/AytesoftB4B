using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.Interfaces;
using DataAccess.Entity;
using System.Data.Entity;

namespace DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }

        public User GetUserWithLogin(string username, string password)
        {
            return context.User.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
        }
        public Context context { get { return _context as Context; } }
    }
}
