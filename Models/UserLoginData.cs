namespace IdentityService.Models
{
    public class UserLoginData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
