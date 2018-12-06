using Platibus.Identity.Helpers;

namespace Platibus.Identity.CreateUserModels
{
    public class CreateUserRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRoles AuthLevel { get; set; }
    }
}