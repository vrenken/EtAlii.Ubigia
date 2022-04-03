// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class System2HostTestContextSignalRTests : IClassFixture<UnitTestContext<System2SignalRHostTestContext>>
    {
        private readonly UnitTestContext<System2SignalRHostTestContext> _context;

        public System2HostTestContextSignalRTests(UnitTestContext<System2SignalRHostTestContext> context)
        {
            _context = context;
        }

        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetSimple()
        {
            // Arrange.
            var port = _context.Host.Ports[TestPort.SignalRUserApi];
            var path = _context.Host.Paths[TestPath.SignalRUserApi];

            // Act.
            var connection = await _context.Host.CreateSignalRConnection($"https://localhost:{port}{path}/UserHub").ConfigureAwait(false);
            var result = await connection.InvokeCoreAsync("GetSimple", typeof(string), Array.Empty<object>()).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result);
            Assert.StartsWith("UserHub_", (result as string)!);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetIncorrect_01()
        {
            // Arrange.
            var port = _context.Host.Ports[TestPort.SignalRUserApi];
            var path = _context.Host.Paths[TestPath.SignalRUserApi];

            // Act.
            var act1 = _context.Host.CreateSignalRConnection($"https://localhost:{port}{path}/UserHub");
            var act2 = _context.Host.CreateSignalRConnection($"https://localhost:{port}{path}/AdminHub");

            // Assert.
            Assert.NotNull(await act1.ConfigureAwait(false));
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await act2.ConfigureAwait(false)).ConfigureAwait(false);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetIncorrect_02()
        {
            // Arrange.
            var port = _context.Host.Ports[TestPort.SignalRUserApi];
            var path = _context.Host.Paths[TestPath.SignalRUserApi];

            // Act.
            var act1 = _context.Host.CreateSignalRConnection($"https://localhost:{port}{path}/UserHub");
            var act2 = _context.Host.CreateSignalRConnection($"https://localhost:{port}{path}/admin/api/UserHub");

            // Assert.
            Assert.NotNull(await act1.ConfigureAwait(false));
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await act2.ConfigureAwait(false)).ConfigureAwait(false);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetComplex()
        {
            // Arrange.
            var port = _context.Host.Ports[TestPort.SignalRUserApi];
            var path = _context.Host.Paths[TestPath.SignalRUserApi];
            var tick = Environment.TickCount;

            // Act.
            var connection = await _context.Host.CreateSignalRConnection($"https://localhost:{port}{path}/UserHub").ConfigureAwait(false);
            var result = await connection.InvokeCoreAsync("GetComplex", typeof(string), new object[] { tick.ToString() }).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result);
            Assert.Equal($"UserHub_{tick}", result);
        }
    }
}
