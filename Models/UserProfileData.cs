using System.ComponentModel.DataAnnotations;

namespace IdentityService.Models
{
    public class UserProfileData
    {
       
        public string LastName { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
    }

}
