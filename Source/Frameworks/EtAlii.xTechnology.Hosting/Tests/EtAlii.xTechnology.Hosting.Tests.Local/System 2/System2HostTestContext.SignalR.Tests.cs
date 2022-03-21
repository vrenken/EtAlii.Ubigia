// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("System hosting tests")]
    public class System2HostTestContextSignalRTests : IClassFixture<System2SignalRHostTestContext>, IAsyncLifetime
    {
        private readonly System2SignalRHostTestContext _context;

        public System2HostTestContextSignalRTests(System2SignalRHostTestContext context)
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
        public async Task System2HostTestContextSignalR_User_Api_GetSimple()
        {
            // Arrange.
            var port = _context.Ports[TestPort.SignalRUserApi];
            var path = _context.Paths[TestPath.SignalRUserApi];

            // Act.
            var connection = await _context.CreateSignalRConnection($"https://localhost:{port}{path}/UserHub").ConfigureAwait(false);
            var result = await connection.InvokeCoreAsync("GetSimple", typeof(string), Array.Empty<object>()).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result);
            Assert.StartsWith("UserHub_", (result as string)!);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetIncorrect_01()
        {
            // Arrange.
            var port = _context.Ports[TestPort.SignalRUserApi];
            var path = _context.Paths[TestPath.SignalRUserApi];

            // Act.
            var act = _context.CreateSignalRConnection($"https://localhost:{port}{path}/AdminHub");

            // Assert.
            await Assert.ThrowsAsync<HttpRequestException>(async () => await act.ConfigureAwait(false)).ConfigureAwait(false);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetIncorrect_02()
        {
            // Arrange.
            var port = _context.Ports[TestPort.SignalRUserApi];
            var path = _context.Paths[TestPath.SignalRUserApi];

            // Act.
            var act = _context.CreateSignalRConnection($"https://localhost:{port}{path}/admin/api/UserHub");

            // Assert.
            await Assert.ThrowsAsync<HttpRequestException>(async () => await act.ConfigureAwait(false)).ConfigureAwait(false);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetComplex()
        {
            // Arrange.
            var port = _context.Ports[TestPort.SignalRUserApi];
            var path = _context.Paths[TestPath.SignalRUserApi];
            var tick = Environment.TickCount;

            // Act.
            var connection = await _context.CreateSignalRConnection($"https://localhost:{port}{path}/UserHub").ConfigureAwait(false);
            var result = await connection.InvokeCoreAsync("GetComplex", typeof(string), new object[] { tick.ToString() }).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result);
            Assert.Equal($"UserHub_{tick}", result);
        }
    }
}
