// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("System hosting tests")]
    public class System2HostTestContextNetCoreTests : IClassFixture<System2RestHostTestContext>, IAsyncLifetime
    {
        private readonly System2RestHostTestContext _context;

        public System2HostTestContextNetCoreTests(System2RestHostTestContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            await _context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);

            await Task.Delay(TimeSpan.FromSeconds(10)).ConfigureAwait(false); // TODO: HT43 - Weird timing issue in hosting tests.
        }

        public async Task DisposeAsync()
        {
            await _context.Stop().ConfigureAwait(false);
        }

        [Fact]
        public async Task System2HostTestContextNetCore_User_Api_Get_1()
        {
            // Arrange.
            var port = _context.Ports[TestPort.RestUserApi];
            var path = _context.Paths[TestPath.RestUserApi];

            // Act.
            using var client = _context.CreateClient();
            var result = await client.GetAsync($"https://localhost:{port}{path}/data").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.StartsWith("UserController_", content);
        }

        [Fact]
        public async Task System2HostTestContextNetCore_User_Api_Get_2()
        {
            // Arrange.
            var port = _context.Ports[TestPort.RestUserApi];
            var path = _context.Paths[TestPath.RestUserApi];
            var tick = Environment.TickCount;

            // Act.
            using var client = _context.CreateClient();
            var result = await client.GetAsync($"https://localhost:{port}{path}/data/GetComplex?postfix={tick}").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.StartsWith($"UserController_{tick}", content);
        }
    }
}
