// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests;

using Xunit;

public class SystemStorageDataClientTests
{
    [Fact]
    public void SystemStorageDataClient_Create()
    {
        // Arrange.

        // Act.
        var client = new SystemStorageDataClient(null);

        // Assert.
        Assert.NotNull(client);
    }
}
