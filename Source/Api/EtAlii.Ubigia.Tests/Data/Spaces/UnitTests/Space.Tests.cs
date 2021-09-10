namespace EtAlii.Ubigia.Tests
{
    using System;
    using Xunit;

    public class SpaceTests
    {
        [Fact]
        public void Space_Create()
        {
            // Arrange.
            var id = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var name = "John";

            // Act.
            var space = new Space
            {
                Id = id,
                AccountId = accountId,
                Name = name,
            };

            // Assert.
            Assert.NotNull(space);
            Assert.Equal(id, space.Id);
            Assert.Equal(accountId, space.AccountId);
            Assert.Equal(name, space.Name);
        }
    }
}
