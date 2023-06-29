using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using MultipleTypesAuthentication.Domain;
using MultipleTypesAuthentication.DTO;
using MultipleTypesAuthentication.Helpers;
using MultipleTypesAuthentication.Repositories.Repository;
using MultipleTypesAuthentication.Services.Interfaces.UserInterface;

namespace MultipleTypesAuthentication.Services
{

    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }



        public Task<UserResponseDTO?> ValidateTokenGoogle(UserTokenGoogle token)
        {
            var payload = GoogleJsonWebSignature.ValidateAsync(token.TokenId, new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new[] { _configuration["ClientIdGoogle"] }

            }).Result;

            var user = _userRepository.FindByEmail(payload.Email);

            UserResponseDTO userResponse = new UserResponseDTO();

            //If it finds the user already registered, it returns to the user

            if (user.Result != null)
            {
                userResponse.user = user.Result;
                userResponse.Code = 1;
                userResponse.Message = "Usuario ya Registrado";
                return Task.FromResult(userResponse);
            }


            //If User not registered, registerd data 
            var googleResponse = CreateUserFromGoogle(payload);

            return Task.FromResult(googleResponse.Result);


        }

        private Task<UserResponseDTO?> CreateUserFromGoogle(GoogleJsonWebSignature.Payload payload)
        {
            var findUser = _userRepository.FindByEmail(payload.Email);
            UserResponseDTO userResponse = new UserResponseDTO();
            if (findUser.Result != null)
            {
                userResponse.user = findUser.Result;
                userResponse.Code = 1;
                userResponse.Message = "El usuario ya esta registrado";
                return Task.FromResult(userResponse);
            }

            var user = new User() { Email = payload.Email };
            var profile = new UserProfile()
            {
                Name = payload.Name,
                LastName = payload.FamilyName,
                UserId = user.Id
            };

            var createUserFromGoogle = _userRepository.CreateUserFromGoogle(user, profile);
            userResponse.user = createUserFromGoogle.Result;
            userResponse.Code = 2;
            userResponse.Message = "El usuario se ha registrado con exito";

            return Task.FromResult(userResponse);


        }

    }
}
