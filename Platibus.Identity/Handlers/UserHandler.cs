using System;
using System.Threading.Tasks;
using EmailValidation;
using Platibus.Identity.CreateUserModels;
using Platibus.Identity.Documents;
using Platibus.Identity.Repositories;

namespace Platibus.Identity.Handlers
{
    public interface IUserHandler
    {
        Task<ResponseWithModel<User>> CreateUser(CreateUserRequestModel createUserRequestModel);
        Task<ResponseWithModel<User>> Login(string email, string password);
    }
    
    public class UserHandler : IUserHandler 
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseWithModel<User>> CreateUser(CreateUserRequestModel createUserModel)
        {
            if (!EmailValidator.Validate(createUserModel.Email))
            {
                return ResponseWithModel<User>.Unsuccessful("This email does not uphold conventions");
            }

            //User to be inserted
            var user = new User
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Email = createUserModel.Email.ToLower(),
                LastLogin = DateTime.UtcNow,
                Password = BCrypt.Net.BCrypt.HashPassword(createUserModel.Password),
                AuthLevel = createUserModel.AuthLevel,
                
            };

            var response = await _userRepository.CreateUser(user);

            if (!response.IsSuccessful)
            {
                return ResponseWithModel<User>.Unsuccessful(response.Message);
            }
            
            return ResponseWithModel<User>.Successfull(user);
        }

        public async Task<ResponseWithModel<User>> Login(string email, string password)
        {
            var result = await _userRepository.Login(email.ToLower());

            if (result == null)
            {
                return ResponseWithModel<User>.Unsuccessful("Invalid email, there exists no user with this email");
            }
            
            //Since there is a user, check if the user password matches the new password
            if (BCrypt.Net.BCrypt.Verify(password, result.Password))
            {
                return ResponseWithModel<User>.Successfull(result);
            }
            
            return ResponseWithModel<User>.Unsuccessful("Incorrect password");
        }
    }
}