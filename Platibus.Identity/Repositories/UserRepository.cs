using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dommel;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Npgsql;
using Platibus.Identity.CreateUserModels;
using Platibus.Identity.Documents;

namespace Platibus.Identity.Repositories
{

    public interface IUserRepository
    {
        Task<Response> CreateUser(User user);
        Task<User> Login(string email);
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
                var result = await conn.QueryAsync<User>("SELECT * FROM Users WHERE email = " + "\'"+ user.Email + "\'");//TODO <-- Discuss SQL injection attack

                if (result.Any())
                {
                    return Response.Unsuccessful("An account with this email already exists");
                }

                var creationResult = await conn.InsertAsync(user);
                
                return Response.Successful();
            }
        }

        public async Task<User> Login(string email)
        {
            using (var conn = new NpgsqlConnection(_connectionString.GetConnectionString()))
            {
                conn.Open();

                var result = await conn.QueryAsync<User>("SELECT * FROM Users WHERE email = " + "\'" + email + "\'"); //TODO : <-- Discuss SQL injection attack

                if (result != null)
                {
                    if (result.Any())
                    {
                        return result.First();  
                    }
                }

                return null;
            }
        }
    }
}