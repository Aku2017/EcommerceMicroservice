namespace IdentityService.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using IdentityService.Models;
    using Microsoft.EntityFrameworkCore;


    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;

        public UserRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetById(string userId)
        {
            return _dbContext.Set<User>()
                .FirstOrDefault(u => u.Id == userId);
        }

        public User GetByUsername(string username)
        {
            return _dbContext.Set<User>()
                .FirstOrDefault(u => u.UserName == username);
        }

        public User GetByEmail(string email)
        {
            return _dbContext.Set<User>()
                .FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Set<User>()
                .ToList();
        }

        public void Add(User user)
        {
            _dbContext.Set<User>().Add(user);
        }

        public void Update(User user)
        {
            _dbContext.Set<User>().Update(user);
        }

        public void Delete(User user)
        {
            _dbContext.Set<User>().Remove(user);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }

}
