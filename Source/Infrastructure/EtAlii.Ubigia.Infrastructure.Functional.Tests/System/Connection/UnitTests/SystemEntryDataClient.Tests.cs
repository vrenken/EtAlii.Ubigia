// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests
{
    using Xunit;

    public class SystemEntryDataClientTests
    {
        [Fact]
        public void SystemEntryDataClient_Create()
        {
            // Arrange.

            // Act.
            var client = new SystemEntryDataClient(null);

            // Assert.
            Assert.NotNull(client);
        }
    }
}
