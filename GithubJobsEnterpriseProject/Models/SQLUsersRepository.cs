using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubJobsEnterpriseProject.Models
{
    public class SQLUsersRepository : IUserRepository
    {
        private readonly UserContext _context;

        public SQLUsersRepository(UserContext userContext)
        {
            this._context = userContext;
        }

        public User Add(User user)
        {
            if (!_context.Users.Contains(user))
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }

            return user;
        }

        public User DeleteUser(int id)
        {
            var userToDelete = _context.Users.Find(id);

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }

            return userToDelete;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public User UpdateUserCredentials(User updatedUser)
        {
            var userToUpdate = _context.Users.Attach(updatedUser);
            userToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updatedUser;
        }
    }
}
