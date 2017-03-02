using Com.Natoma.Adpq.Prototype.Business.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Natoma.Adpq.Prototype.Business.Test.TestUtils
{
    public class DbContextUtils
    {
        public static DbContextOptions<adpq2adpqContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<adpq2adpqContext>();
            builder.UseInMemoryDatabase().UseInternalServiceProvider(serviceProvider);

            return builder.Options;

        }
    }
}
