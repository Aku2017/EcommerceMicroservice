using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using IdentityService.Models;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManageController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ManageController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            // Return user profile information
            return Ok(new
            {
                user.UserName,
                user.Email
                // Include any other properties you want to expose
            });
        }

        [HttpPost("changepassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordData changePasswordData)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound();
                }

                var result = await _userManager.ChangePasswordAsync(user, changePasswordData.CurrentPassword, changePasswordData.NewPassword);

                if (result.Succeeded)
                {
                    // Password changed successfully
                    return Ok();
                }

                // Failed to change the password
                return BadRequest(result.Errors);
            }

            // Model validation failed
            return BadRequest(ModelState);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateUserProfile(UserProfileData profileData)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            // Update the user profile data
            user.FirstName = profileData.FirstName;
            user.LastName = profileData.LastName;
            // Update other profile properties as needed

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }


        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(EmailVerificationData verificationData)
        {
            var user = await _userManager.FindByEmailAsync(verificationData.Email);

            if (user == null)
            {
                return NotFound();
            }

            // Verify the email using the verification token
            var result = await _userManager.ConfirmEmailAsync(user, verificationData.Token);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        // Add other methods for account management functionalities here
        // such as updating profile information, email verification, etc.
    }
}
