namespace Platibus.Identity.UpdateUserModel
{
    public class UpdateUserRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int AuthLevel { get; set; }
    }
}