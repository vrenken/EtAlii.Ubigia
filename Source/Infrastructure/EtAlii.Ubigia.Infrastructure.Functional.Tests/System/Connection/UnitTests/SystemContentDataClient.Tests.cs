// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests
{
    using Xunit;

    public class SystemContentDataClientTests
    {
        [Fact]
        public void SystemContentDataClient_Create()
        {
            // Arrange.

            // Act.
            var client = new SystemContentDataClient(null);

            // Assert.
            Assert.NotNull(client);
        }
    }
}
