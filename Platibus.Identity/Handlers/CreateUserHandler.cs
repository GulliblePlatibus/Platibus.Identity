using System.Threading.Tasks;
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

        public async Task<Response> CreateUser(CreateUserRequestModel createUserRequestModel)
        {
            await _userRepository.CreateUser(new User());

            return Response.Successful();
        }
    }
}