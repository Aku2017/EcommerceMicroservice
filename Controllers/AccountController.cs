using IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration; 
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationData registrationData)
        {
            if (ModelState.IsValid)
            {
                // Create a new User instance
                var user = new User { UserName = registrationData.Email, Email = registrationData.Email, 
                                        FirstName = registrationData.FirstName, LastName = registrationData.LastName,
                                          PhoneNumber = registrationData.PhoneNumber };

                // Attempt to create the user
                var result = await _userManager.CreateAsync(user, registrationData.Password);

                if (result.Succeeded)
                {
                    // User registration successful
                    return Ok();
                }

                // User registration failed
                return BadRequest(result.Errors);
            }

            // Model validation failed
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginData loginData)
        {
            if (ModelState.IsValid)
            {
                // Attempt to sign in the user
                Microsoft.AspNetCore.Identity.SignInResult result = null;
                try
                {
                    result = await _signInManager.PasswordSignInAsync(loginData.UserName, loginData.Password, loginData.RememberMe, lockoutOnFailure: false);
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                 }

                if (result.Succeeded)
                {
                    // User login successful
                    return Ok();
                }
                else
                {
                    Console.WriteLine(result);
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }

                // User login failed
                return BadRequest("Invalid login attempt");
            }

            // Model validation failed
            return BadRequest(ModelState);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await _signInManager.SignOutAsync();

            // User logout successful
            return Ok();
        }
    }
}

