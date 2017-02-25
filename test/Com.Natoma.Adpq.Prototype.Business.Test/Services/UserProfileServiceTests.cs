using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Data;
using Com.Natoma.Adpq.Prototype.Business.Models.Request;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;
using Com.Natoma.Adpq.Prototype.Business.Services;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Com.Natoma.Adpq.Prototype.Business.Test.TestUtils;
using Moq;
using Xunit;

namespace Com.Natoma.Adpq.Prototype.Business.Test.Services
{
    public class UserProfileServiceTests
    {
        [Fact]
        public async void UserProfileService_Create_Success()
        {
            var options = DbContextUtils.CreateNewContextOptions();
            var context = new adpq2adpqContext(options);
            var mockGeoCodeService = new Mock<IGeoCodeService>();
            mockGeoCodeService.Setup(x => x.GetGeoLocation(It.IsAny<string>(), It.IsAny<string>(), 
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(() => new LatLongSet())
            ;
            PopulateContext(context);
            var service = new UserProfileService(context, mockGeoCodeService.Object);

            var newUser = new UserProfileViewModel
            {
                FirstName = "test1",
                LastName = "test1Last",
                AddressLine1 = "1234 Main",
                City = "Anytown",
                Zipcode = "91111",
                Email = "test2@test.com",
                Password = "password1",
                State = "CA",
                IsAdmin = false,
                IsEmailNotifications = true,
                IsSms = true,
                Phone = "1234567890"
            };

            var result = await service.Create(newUser);
            Assert.True(result.State == RequestStateEnum.Success);
            Assert.True(result.Data != null);
        }

        [Fact]
        public async void UserProfileService_Create_EmailExists()
        {
            var options = DbContextUtils.CreateNewContextOptions();
            var context = new adpq2adpqContext(options);
            var mockGeoCodeService = new Mock<IGeoCodeService>();
            mockGeoCodeService.Setup(x => x.GetGeoLocation(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(() => new LatLongSet())
            ;
            PopulateContext(context);
            var service = new UserProfileService(context, mockGeoCodeService.Object);

            var newUser = new UserProfileViewModel
            {
                FirstName = "test1",
                LastName = "test1Last",
                AddressLine1 = "1234 Main",
                City = "Anytown",
                Zipcode = "91111",
                Email = "test@test.com", // this email exists
                Password = "password1",
                State = "CA",
                IsAdmin = false,
                IsEmailNotifications = true,
                IsSms = true,
                Phone = "1234567890"
            };

            var result = await service.Create(newUser);
            Assert.True(result.State == RequestStateEnum.Failed);
            Assert.True(result.Data == null);
        }

        [Fact]
        public async void UserProfileService_Update_Success()
        {
            var options = DbContextUtils.CreateNewContextOptions();
            var context = new adpq2adpqContext(options);
            var mockGeoCodeService = new Mock<IGeoCodeService>();
            mockGeoCodeService.Setup(x => x.GetGeoLocation(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(() => new LatLongSet())
            ;
            PopulateContext(context);
            var service = new UserProfileService(context, mockGeoCodeService.Object);

            var existingUser = new UserProfileViewModel
            {
                UserProfileId = 100,
                FirstName = "test1",
                LastName = "test1Last",
                AddressLine1 = "1234 Main",
                City = "Anytown",
                Zipcode = "91111",
                Email = "test@test.com",
                Password = "password1",
                State = "CA",
                IsAdmin = false,
                IsEmailNotifications = true,
                IsSms = true,
                Phone = "1234567890"
            };

            var result = await service.Update(existingUser);
            Assert.True(result.State == RequestStateEnum.Success);
            Assert.True(result.Data != null);
        }

        [Fact]
        public async void UserProfileService_Update_NotFound()
        {
            var options = DbContextUtils.CreateNewContextOptions();
            var context = new adpq2adpqContext(options);
            var mockGeoCodeService = new Mock<IGeoCodeService>();
            mockGeoCodeService.Setup(x => x.GetGeoLocation(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(() => new LatLongSet())
            ;
            PopulateContext(context);
            var service = new UserProfileService(context, mockGeoCodeService.Object);

            var existingUser = new UserProfileViewModel
            {
                UserProfileId = 99,
                FirstName = "test1",
                LastName = "test1Last",
                AddressLine1 = "1234 Main",
                City = "Anytown",
                Zipcode = "91111",
                Email = "test2@test.com", // this email exists
                Password = "password1",
                State = "CA",
                IsAdmin = false,
                IsEmailNotifications = true,
                IsSms = true,
                Phone = "1234567890"
            };

            var result = await service.Update(existingUser);
            Assert.True(result.State == RequestStateEnum.Failed);
            Assert.True(result.Data == null);
        }

        [Fact]
        public async void UserProfileService_Get()
        {
            var options = DbContextUtils.CreateNewContextOptions();
            var context = new adpq2adpqContext(options);
            var mockGeoCodeService = new Mock<IGeoCodeService>();
            PopulateContext(context);
            var service = new UserProfileService(context, mockGeoCodeService.Object);
            var result = await service.Get(1);

            Assert.True(result != null);
        }

        [Fact]
        public async void UserProfileService_GetByEmail_Success()
        {
            var options = DbContextUtils.CreateNewContextOptions();
            var context = new adpq2adpqContext(options);
            var mockGeoCodeService = new Mock<IGeoCodeService>();
            PopulateContext(context);
            var service = new UserProfileService(context, mockGeoCodeService.Object);
            var result = await service.Get("test@test.com","password1");

            Assert.True(result != null);
            Assert.True(result.State == RequestStateEnum.Success);
        }

        [Fact]
        public async void UserProfileService_GetByEmail_Failure()
        {
            var options = DbContextUtils.CreateNewContextOptions();
            var context = new adpq2adpqContext(options);
            var mockGeoCodeService = new Mock<IGeoCodeService>();
            PopulateContext(context);
            var service = new UserProfileService(context, mockGeoCodeService.Object);
            var result = await service.Get("test@test.com", "password2");

            Assert.True(result != null);
            Assert.True(result.State == RequestStateEnum.NotAuth);
        }

        private void PopulateContext(adpq2adpqContext context)
        {
            var user1 = new User
            {
                UserId = 100,
                FirstName = "test100",
                Password = "cyojDCwVhwk0d3nmDAMT6C5W0KIB4dr6E3TbbhVAwu8=",  // password1
                PasswordSalt = "retYGs9honEytltV51iLsg==",
                Email = "test@test.com"
            };
            context.User.Add(user1);
            context.SaveChanges();
        }
    }
}
