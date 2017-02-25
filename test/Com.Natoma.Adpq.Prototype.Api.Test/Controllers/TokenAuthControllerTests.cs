using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Api.Controllers;
using Com.Natoma.Adpq.Prototype.Business.Models.Request;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Com.Natoma.Adpq.Prototype.Api.Test.Controllers
{
    public class TokenAuthControllerTests
    {
        [Fact]
        public async void TokenAuthController_GetAuthToken_Success()
        {
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => new RequestResult
                {
                    State = RequestStateEnum.Success,
                    Data = new UserProfileViewModel
                    {
                        UserProfileId = 1,
                        IsAdmin = false,
                        Email = "test@test.com"
                    }
                }).Verifiable();
            var controller = new TokenAuthController(mockUserProfileService.Object);
            var user = new UserProfileViewModel
            {
                FirstName = "test",
                LastName = "test",
                Email = "email@email.com"
            };
            

            var result = await controller.GetAuthToken(user) as OkObjectResult;
            var requestResult = result.Value as RequestResult;

            mockUserProfileService.Verify(m => m.Get(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.True(requestResult.State == RequestStateEnum.Success);
            
        }

        [Fact]
        public async void TokenAuthController_GetAuthToken_Failure()
        {
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => new RequestResult()).Verifiable();
            var controller = new TokenAuthController(mockUserProfileService.Object);
            var user = new UserProfileViewModel
            {
                FirstName = "test",
                LastName = "test",
                Email = "email@email.com"
            };


            var result = await controller.GetAuthToken(user) as OkObjectResult;
            var requestResult = result.Value as RequestResult;

            mockUserProfileService.Verify(m => m.Get(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.True(requestResult.State == RequestStateEnum.Failed);

        }
    }
}
