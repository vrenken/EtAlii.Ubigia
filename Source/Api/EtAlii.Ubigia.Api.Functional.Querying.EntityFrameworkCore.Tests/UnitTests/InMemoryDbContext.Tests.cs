namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Tests
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    //you have to label the class with this or it is never scanned for methods
    public class InMemoryDbContextTests
    {
        [Fact]
        public void InMemoryDbContext_Create()
        {
            // Arrange.
            var options = new DbContextOptionsBuilder<InMemoryTestContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            var customer = new Customer
            {
                FirstName = "Elizabeth",
                LastName = "Lincoln",
                Address = "23 Tsawassen Blvd."
            };

            // Act.
            using var context1 = new InMemoryTestContext(options);
            context1.Customers.Add(customer);
            context1.SaveChanges();

            using var context2 = new InMemoryTestContext(options);
            var result = context2.Customers.SingleOrDefault(c => c.FirstName == customer.FirstName);
            
            // Assert.
            Assert.NotNull(result);
            Assert.Equal(customer.CustomerId, result.CustomerId);
            Assert.Equal(customer.Address, result.Address);
            Assert.Equal(customer.LastName, result.LastName);
            
        }
    }
}