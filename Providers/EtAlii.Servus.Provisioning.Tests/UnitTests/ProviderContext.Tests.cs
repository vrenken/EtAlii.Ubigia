﻿namespace EtAlii.Servus.Provisioning.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    public class ProviderContext_Tests
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
