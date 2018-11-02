using System.Collections.Generic;
using System.Threading.Tasks;
using Platibus.Identity.CreateUserModels;
using Platibus.Identity.DomainModels;

namespace Platibus.Identity.Repositories
{

    public interface IUserRepository
    {
        Task<bool> createUser(CreateUserRequestModel createUserRequestModel);
    }
    public class UserRepository : IUserRepository
    {
        private static HashSet<User> _users = new HashSet<User>();

        public Task<bool> createUser(CreateUserRequestModel createUserRequestModel)
        {
            var user = new User(createUserRequestModel.Name, createUserRequestModel.Email,
                createUserRequestModel.Password);

            return Task.FromResult(_users.Add(user));
        }
    }
}
