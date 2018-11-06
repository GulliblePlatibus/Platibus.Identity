namespace Platibus.Identity.CreateUserModels
{
    public class CreateUserRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int AuthLevel { get; set; }
    }
}