using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Dapper.FluentMap.Dommel.Mapping;

namespace Platibus.Identity
{
    public class UserMap : DommelEntityMap<User>
    {
        public UserMap()
        {
            ToTable("Users");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Email).ToColumn("email");
            Map(x => x.Password).ToColumn("password");
            Map(x => x.Created).ToColumn("created");
            Map(x => x.LastLogin).ToColumn("lastlogin");
        }
    }
    
    public class User
    {
        //Primary key
        public Guid Id { get; set; }
        
        //Email should also be unique
        public string Email { get; set; }
        
        //Hashed password
        public string Password { get; set; }
        
        //Date of creation
        public DateTime Created { get; set; }
        
        //Date of last login
        public DateTime LastLogin { get; set; }
        
    }
}