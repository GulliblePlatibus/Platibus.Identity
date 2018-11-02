using System.Threading.Tasks;
using Platibus.Identity.CreateUserModels;
using Platibus.Identity.Repositories;

namespace Platibus.Identity.Handlers
{
    public interface ICreateUserHandler
    {
        Task<bool> CreateUser(CreateUserRequestModel createUserRequestModel);
    }
    
    public class CreateUserHandler : ICreateUserHandler 
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUser(CreateUserRequestModel createUserRequestModel)
        {
            return await _userRepository.createUser(createUserRequestModel);
        }
    }
}