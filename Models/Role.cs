using Microsoft.AspNetCore.Identity;

namespace IdentityService.Models
{
    public class Role : IdentityRole
    {
        public string Name { get; set; }
    }

}
