namespace EtAlii.Ubigia.Provisioning.Tests
{
    using System;
    using Xunit;

    public class ProviderContextTests
    {
        [Fact]
        public void ProviderContext_Create()
        {
            // Arrange.

            // Act.
            var context = new ProviderContext(null, null, null);

            // Assert.
            Assert.NotNull(context);
        }
    }
}
