using Google.Apis.Auth;
using MultipleTypesAuthentication.DTO;
using MultipleTypesAuthentication.Helpers;

namespace MultipleTypesAuthentication.Services.Interfaces.UserInterface
{
    public interface IUserService
    {
        Task<UserResponseDTO?> ValidateTokenGoogle(UserTokenGoogle token);
    }
}
