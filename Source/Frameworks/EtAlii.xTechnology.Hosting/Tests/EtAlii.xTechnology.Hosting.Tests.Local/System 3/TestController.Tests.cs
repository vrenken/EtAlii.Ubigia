// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

public class TestControllerTests
{
    [Fact]
    public async Task TestController_Call_Simple_Get()
    {
        // Arrange.
        var webHostBuilder =
            new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<Startup>();

        using var server = new TestServer(webHostBuilder);
        using var client = server.CreateClient();

        // Act.
        var result = await client.GetStringAsync("/api/test").ConfigureAwait(false);

        // Assert.
        Assert.Equal("[\"value1\",\"value2\"]", result);
    }
}
