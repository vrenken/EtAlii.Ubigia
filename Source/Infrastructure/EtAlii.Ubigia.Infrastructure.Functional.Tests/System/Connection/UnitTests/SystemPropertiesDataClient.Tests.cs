// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests
{
    using Xunit;

    public class SystemPropertiesDataClientTests
    {
        [Fact]
        public void SystemPropertiesDataClient_Create()
        {
            // Arrange.

            // Act.
            var client = new SystemPropertiesDataClient(null);

            // Assert.
            Assert.NotNull(client);
        }
    }
}
