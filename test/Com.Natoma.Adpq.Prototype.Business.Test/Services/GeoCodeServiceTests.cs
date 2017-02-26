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
    public class GeoCodeServiceTests
    {
        [Fact]
        public async void GeoCodeServiceTest_GetUsersInRadius()
        {
            var options = DbContextUtils.CreateNewContextOptions();
            var context = new adpq2adpqContext(options);
            PopulateContext(context);
            var service = new GeoCodeService(context);

            var result = service.GetUsersInRadius(38.616777, -121.3601732, 1);
            
            Assert.True(result.Count == 1);

            var result2 = service.GetUsersInRadius(38.616777, -121.3601732, 15);

            Assert.True(result2.Count == 2);
        }

        private void PopulateContext(adpq2adpqContext context)
        {
            var user1 = new User
            {
                UserId = 120,
                FirstName = "Less than a mile",
                Latitude = 38.615967,
                Longitude = -121.360219
            };
            var user2 = new User
            {
                UserId = 250,
                FirstName = "less than 20 miles",
                Latitude = 38.576486,
                Longitude = -121.493858
            };
            context.User.Add(user1);
            context.User.Add(user2);
            context.SaveChanges();
        }
    }
}
