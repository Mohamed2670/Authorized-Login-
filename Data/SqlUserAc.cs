using Microsoft.EntityFrameworkCore;
using MwTesting.Model;

namespace MwTesting.Data
{
    public class SqlUserAc(UserContext userContext) : IUserAc
    {
        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            userContext.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            userContext.Users.Remove(user);
        }

        public IEnumerable<User> GetAllUsers()
        {

            return userContext.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return userContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return userContext.SaveChanges()>=0;
        }
    }
}