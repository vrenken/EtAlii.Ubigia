// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Tests
{
    using Xunit;

    public class SystemAccountDataClientTests
    {
        [Fact]
        public void SystemAccountDataClient_Create()
        {
            // Arrange.

            // Act.
            var client = new SystemAccountDataClient(null);

            // Assert.
            Assert.NotNull(client);
        }
    }
}
