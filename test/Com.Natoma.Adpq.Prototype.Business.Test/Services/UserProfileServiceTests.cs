using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Data;
using Com.Natoma.Adpq.Prototype.Business.Services;
using Com.Natoma.Adpq.Prototype.Business.Test.TestUtils;
using Xunit;

namespace Com.Natoma.Adpq.Prototype.Business.Test.Services
{
    public class UserProfileServiceTests
    {
        [Fact]
        public async void UserProfileService_Get()
        {
            var options = DbContextUtils.CreateNewContextOptions();
            var context = new adpq2adpqContext(options);
            PopulateContext(context);
            var service = new UserProfileService(context);
            var result = await service.Get(1);

            Assert.True(result != null);
        }

        private void PopulateContext(adpq2adpqContext context)
        {
            var user1 = new User
            {
                UserId = 1,
                FirstName = "test1"
            };
            context.User.Add(user1);
            context.SaveChanges();
        }
    }
}
