using ProsaApp.Domain.Types;

namespace ProsaApp.Test
{
    public class CustomerTests
    {

        [Fact]
        public void CustomerTests_ShouldReturnNewCustomerObject()
        {
            var d = DateTime.Now;
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                Name = "John Doe",
                Age = 30,
                Country = "USA",
                Revenue = 1000.00,
                CreatedDate = d,
                IsActive = true,
                Tags = new List<string> { "Tag1", "Tag2" }
            };
            // Act
            var result = customer;
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal(30, result.Age);
            Assert.Equal("USA", result.Country);
            Assert.Equal(1000.00, result.Revenue);
            Assert.Equal(d, result.CreatedDate);
            Assert.True(result.IsActive);
            Assert.Equal(2, result.Tags.Count);

        }
    }
}
