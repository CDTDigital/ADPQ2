using Com.Natoma.Adpq.Prototype.Api.Controllers;
using Com.Natoma.Adpq.Prototype.Business.Models.Request;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Com.Natoma.Adpq.Prototype.Api.Test.Controllers
{
    
    public class UserProfileControllerTests
    {
        [Fact]
        public async void UserProfileController_GetUser()
        {
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(() => new RequestResult()).Verifiable();
            var controller = new UserProfileController(mockUserProfileService.Object);

            var result = await controller.Get(1);

            mockUserProfileService.Verify(m => m.Get(It.IsAny<int>()), Times.Once);
            Assert.NotNull(result);
        }


        [Fact]
        public async void UserProfileController_CreateUser()
        {
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.Create(It.IsAny<UserProfileViewModel>()))
                .ReturnsAsync(() => new RequestResult()).Verifiable();
            var controller = new UserProfileController(mockUserProfileService.Object);
            
            var user = new UserProfileViewModel
            {
                FirstName = "test",
                LastName = "test",
                Email = "email@email.com"
            };
            
            var result = await controller.Post(user);

            mockUserProfileService.Verify(m => m.Create(It.IsAny<UserProfileViewModel>()), Times.Once);
            Assert.NotNull(result);
           
        }

        [Fact]
        public async void UserProfileController_UpdateUser()
        { 
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.Update(It.IsAny<UserProfileViewModel>()))
                .ReturnsAsync(() => new RequestResult()).Verifiable();
            var controller = new UserProfileController(mockUserProfileService.Object);

            var user = new UserProfileViewModel
            {
                UserProfileId = 1,
                FirstName = "test",
                LastName = "test",
                Email = "email@email.com"
            };

            var result = await controller.Put(1,user);

            mockUserProfileService.Verify(m => m.Update(It.IsAny<UserProfileViewModel>()), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async void UserProfileController_UpdateUser_Mismatch()
        {
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.Update(It.IsAny<UserProfileViewModel>()))
                .ReturnsAsync(() => new RequestResult()).Verifiable();
            var controller = new UserProfileController(mockUserProfileService.Object);

            var user = new UserProfileViewModel
            {
                UserProfileId = 2,
                FirstName = "test",
                LastName = "test",
                Email = "email@email.com"
            };

            var result = await controller.Put(1, user);

            mockUserProfileService.Verify(m => m.Update(It.IsAny<UserProfileViewModel>()), Times.Never);
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
