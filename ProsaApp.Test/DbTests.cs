using ProsaApp.Domain.Types;

namespace ProsaApp.Test
{
    public class DbTests
    {
        [Fact]
        public void DbTests_ShouldReturnObjects()
        {

            var contxt = new ProsaApp.Data.DataAccess.Services.DataContext(new ProsaApp.Data.DataAccess.Mock.MockDataAccessService());
            var customers = contxt.GetAllCustomers(new Microsoft.Extensions.Logging.Abstractions.NullLogger<ProsaApp.Data.DataAccess.Services.DataContext>());
            Assert.NotNull(customers);

        }
        [Fact]
        public void DbTests_ShouldReturnFiveObjects()
        {

            var contxt = new ProsaApp.Data.DataAccess.Services.DataContext(new ProsaApp.Data.DataAccess.Mock.MockDataAccessService());
            var customers = contxt.GetAllCustomers(new Microsoft.Extensions.Logging.Abstractions.NullLogger<ProsaApp.Data.DataAccess.Services.DataContext>());
            Assert.Equal(5, customers.Count());

        }

        [Fact]
        public void DbTests_ShouldReturnObjectsWithNames()
        {

            var contxt = new ProsaApp.Data.DataAccess.Services.DataContext(new ProsaApp.Data.DataAccess.Mock.MockDataAccessService());
            var customers = contxt.GetAllCustomers(new Microsoft.Extensions.Logging.Abstractions.NullLogger<ProsaApp.Data.DataAccess.Services.DataContext>());
            Assert.NotNull(customers);
            Assert.All(customers, c => Assert.NotNull(c.Name));
            Assert.All(customers, c => Assert.NotEmpty(c.Name));

        }

    }
}
