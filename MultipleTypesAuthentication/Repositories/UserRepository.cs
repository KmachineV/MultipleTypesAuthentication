using Microsoft.EntityFrameworkCore;
using MultipleTypesAuthentication.Data;
using MultipleTypesAuthentication.Domain;
using MultipleTypesAuthentication.Repositories.Repository;

namespace MultipleTypesAuthentication.Repositories
{
    public class UserRepository : IUserRepository

    {

        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;            
        }

        public async Task<User?> CreateUserFromGoogle(User user, UserProfile userProfile)
        {
           var saveUser = _context.User.Add(user);
           var saveUserProfile = _context.UserProfile.Add(userProfile);
           await _context.SaveChangesAsync();
           return saveUser.Entity;
        }

        public async Task<User?> FindByEmail(string email)
        {
            var findUser = _context.User.
                Where(a => a.Email == email).Include(a=> a.UserProfile).FirstOrDefaultAsync(); 

            if (findUser != null)
            {
                return await findUser;
            }
            return null;
        }

        public Task<User> LoginUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
