using MultipleTypesAuthentication.Domain;

namespace MultipleTypesAuthentication.Repositories.Repository
{
    public interface IUserRepository
    {
        Task<User> LoginUser(User user);
        Task<User?> FindByEmail(string email);
        Task<User> CreateUserFromGoogle(User user, UserProfile userProfile);
    }
}
