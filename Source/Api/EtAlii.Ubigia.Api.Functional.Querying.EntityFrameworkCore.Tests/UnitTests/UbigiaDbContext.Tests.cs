namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Tests
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    //you have to label the class with this or it is never scanned for methods
    public class UbigiaDbContextTests
    {
        [Fact]//[Fact(Skip = "Not implemented yet")]
        public void UbigiaDbContext_Create()
        {
            // Arrange.
            var options = new DbContextOptionsBuilder<UbigiaTestContext>()
                .UseUbigiaContext<GrpcTransport>("http://localhost:123", "TestStorage", "TestUser", "test123")
                .Options;
            
            var customer = new Customer
            {
                FirstName = "Elizabeth",
                LastName = "Lincoln",
                Address = "23 Tsawassen Blvd."
            };

            // Act.
            using var context1 = new UbigiaTestContext(options);
            context1.Customers.Add(customer);
            context1.SaveChanges();

            using var context2 = new UbigiaTestContext(options);
            var result = context2.Customers.SingleOrDefault(c => c.FirstName == customer.FirstName);
            
            // Assert.
            Assert.NotNull(result);
            Assert.Equal(customer.CustomerId, result.CustomerId);
            Assert.Equal(customer.Address, result.Address);
            Assert.Equal(customer.LastName, result.LastName);
            
        }
    }
}