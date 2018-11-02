using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Platibus.Identity
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid ID { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastLogin { get; set; }
        
    }
}