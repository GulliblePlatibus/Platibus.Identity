using System.Collections.Generic;
using System.Threading.Tasks;
using Platibus.Identity.CreateUserModels;

namespace Platibus.Identity.Repositories
{

    public interface IUserRepository
    {
        Task CreateUser(User user);
    }
    public class UserRepository : IUserRepository
    {
        private static HashSet<User> _users = new HashSet<User>();

        public async Task CreateUser(User user)
        {
           
        }
    }
}
