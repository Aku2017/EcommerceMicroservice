using IdentityService.Models;

namespace IdentityService.Repository
{
    public interface IUserRepository
    {
        User GetById(string userId);
        User GetByUsername(string username);
        User GetByEmail(string email);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        void SaveChanges();
    }

}
