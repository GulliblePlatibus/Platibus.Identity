using System.Threading.Tasks;

namespace Platibus.Identity.Handlers
{
    public interface IUserHandler
    {
        Task<bool> ValidateUserCredentials(string username, string password);
    }
    
    public class UserHandler : IUserHandler
    {
        public async Task<bool> ValidateUserCredentials(string username, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}