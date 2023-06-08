using IdentityService.Controllers;
using IdentityService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace TestProject1
{
    public class IdentityServiceTests
    {
        //[Fact]
        //public void Authenticate_ValidCredentials_ReturnsTrue()
        //{
        //  //  var identityService = new IdentityService();
        //    var user = new User
        //    {
        //        UserName = "michael",
        //        Email = "odiong@mail.com",
        //        FirstName = "odiong",
        //        LastName = "",
        //        PhoneNumber = "09097666667"
        //    };

        //   // bool isAuthenticated = _userManager.CreateAsync(user, "Anayochi@total30");
        //    // Act
        //   //  = identityService.Authenticate("username", "password");

        //    // Assert
        //    Assert.True(isAuthenticated);
        //}

        [Fact]
        public async Task RegisterUser_ValidData_ReturnsOk()
        {
            // Arrange
            //var userManagerMock = new Mock<UserManager<User>>();
            ////var userManagerMock = new Mock<UserManager<User>>();
            //var signInManagerMock = new Mock<SignInManager<User>>();
            var userStoreMock = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
          //  var signInManagerMock = new Mock<SignInManager<User>>(userManagerMock.Object, null, null, null, null, null, null);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var signInManagerMock = new Mock<SignInManager<User>>(userManagerMock.Object, httpContextAccessorMock.Object);
    // other required parameter);


            var configurationMock = new Mock<IConfiguration>();
           // userManagerMock.Setup(...); // Set up any necessary mocks or expectations

            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object, configurationMock.Object);

            var registrationData = new UserRegistrationData
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789",
                Password = "Test123!"
            };

            // Act
            var result = await controller.Register(registrationData);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}