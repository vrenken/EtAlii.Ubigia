namespace EtAlii.Ubigia.Tests
{
    using System;
    using Xunit;

    public class AccountTests
    {
        [Fact]
        public void Account_Create()
        {
            // Arrange.
            var created = DateTime.Now;
            var id = Guid.NewGuid();
            var name = "John";
            var password = "baadfood";
            var roles = new[] {Role.Admin};
            var updated = DateTime.Now;

            // Act.
            var account = new Account
            {
                Created = created,
                Id = id,
                Name = name,
                Password = password,
                Roles = roles,
                Updated = updated,
            };

            // Assert.
            Assert.NotNull(account);
            Assert.Equal(created, account.Created);
            Assert.Equal(id, account.Id);
            Assert.Equal(name, account.Name);
            Assert.Equal(password, account.Password);
            Assert.Single(account.Roles, Role.Admin);
            Assert.Equal(updated, account.Updated);
        }
    }
}
