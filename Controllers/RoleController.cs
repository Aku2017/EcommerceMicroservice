using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using IdentityService.Models;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Restrict access to users with the "Admin" role
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                var role = new Role { Name = roleName };

                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return Ok();
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Role name cannot be empty");
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return Ok();
                }

                return BadRequest(result.Errors);
            }

            return NotFound();
        }

        // Add other methods for role management here
        // such as updating role information, assigning/removing roles to/from users, etc.
    }
}
