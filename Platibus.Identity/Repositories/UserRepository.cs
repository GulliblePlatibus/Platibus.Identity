using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using Platibus.Identity.CreateUserModels;
using Platibus.Identity.Documents;

namespace Platibus.Identity.Repositories
{

    public interface IUserRepository
    {
        Task<Response> CreateUser(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IConnectionString _connectionString;

        public UserRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }
            
            
        public async Task<Response> CreateUser(User user)
        {
            using (var conn = new NpgsqlConnection(_connectionString.GetConnectionString()))
            {
                conn.Open();
                
            }
            return null;
        }
    }
}