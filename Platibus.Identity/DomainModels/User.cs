using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Platibus.Identity.DomainModels
{
    public class User : IEqualityComparer<User>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public User(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }



        public bool Equals(User x, User y)
        {
            return x.Email.Equals(y.Email);
        }

        public int GetHashCode(User obj)
        {
            throw new System.NotImplementedException();
        }
    }
}