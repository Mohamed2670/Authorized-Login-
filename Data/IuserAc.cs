using MwTesting.Model;

namespace MwTesting.Data
{
    public interface IUserAc
    {
        bool SaveChanges();
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void CreateUser(User user);
        void DeleteUser(User user);
    }
}