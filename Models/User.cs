using Microsoft.AspNetCore.Identity;

namespace IdentityService.Models
{
    public class User: IdentityUser
    {
       
        public string Email { get; set; }
            
        // Additional properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public bool IsActive { get; set; }
        //public DateTime RegistrationDate { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }

}
