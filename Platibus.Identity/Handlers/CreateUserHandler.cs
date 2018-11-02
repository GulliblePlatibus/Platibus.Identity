using System;
using System.Threading.Tasks;
using EmailValidation;
using Platibus.Identity.CreateUserModels;
using Platibus.Identity.Documents;
using Platibus.Identity.Repositories;

namespace Platibus.Identity.Handlers
{
    public interface ICreateUserHandler
    {
        Task<Response> CreateUser(CreateUserRequestModel createUserRequestModel);
    }
    
    public class CreateUserHandler : ICreateUserHandler 
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> CreateUser(CreateUserRequestModel createUserModel)
        {
            if (!EmailValidator.Validate(createUserModel.Email))
            {
                return Response.Unsuccessful("This email does not uphold conventions");
            } 
            
            return await _userRepository.CreateUser(new User
            {
                Created = DateTime.UtcNow, 
                Email = createUserModel.Email,
                ID = Guid.NewGuid(),
                LastLogin = DateTime.UtcNow,
                Password = BCrypt.Net.BCrypt.HashPassword(createUserModel.Password)
                
            });
            

        }
    }
}