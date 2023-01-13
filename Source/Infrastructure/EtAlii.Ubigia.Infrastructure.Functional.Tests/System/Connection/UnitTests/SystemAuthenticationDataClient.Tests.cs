// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests;

using Xunit;

public class SystemAuthenticationDataClientTests
{
    [Fact]
    public void SystemAuthenticationDataClient_Create()
    {
        // Arrange.

        // Act.
        var client = new SystemAuthenticationDataClient(null);

        // Assert.
        Assert.NotNull(client);
    }
}
