// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public class System2HostTestContextSignalRTests
    {
        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetSimple()
        {
            // Arrange.
            var context = new LocalHostTestContext(ConfigurationFiles.HostSettingsSystems2VariantSignalR, ConfigurationFiles.ClientSettings);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.SignalRUserApi];
            var path = context.Paths[TestPath.SignalRUserApi];

            // Act.
            var connection = await context.CreateSignalRConnection($"https://localhost:{port}{path}/UserHub").ConfigureAwait(false);
            var result = await connection.InvokeCoreAsync("GetSimple", typeof(string), Array.Empty<object>()).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result);
            Assert.StartsWith("UserHub_", (result as string)!);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetIncorrect_01()
        {
            // Arrange.
            var context = new LocalHostTestContext(ConfigurationFiles.HostSettingsSystems2VariantSignalR, ConfigurationFiles.ClientSettings);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.SignalRUserApi];
            var path = context.Paths[TestPath.SignalRUserApi];

            // Act.
            var act = context.CreateSignalRConnection($"https://localhost:{port}{path}/AdminHub");

            // Assert.
            await Assert.ThrowsAsync<HttpRequestException>(async () => await act.ConfigureAwait(false)).ConfigureAwait(false);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetIncorrect_02()
        {
            // Arrange.
            var context = new LocalHostTestContext(ConfigurationFiles.HostSettingsSystems2VariantSignalR, ConfigurationFiles.ClientSettings);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.SignalRUserApi];
            var path = context.Paths[TestPath.SignalRUserApi];

            // Act.
            var act = context.CreateSignalRConnection($"https://localhost:{port}{path}/admin/api/UserHub");

            // Assert.
            await Assert.ThrowsAsync<HttpRequestException>(async () => await act.ConfigureAwait(false)).ConfigureAwait(false);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetComplex()
        {
            // Arrange.
            var context = new LocalHostTestContext(ConfigurationFiles.HostSettingsSystems2VariantSignalR, ConfigurationFiles.ClientSettings);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.SignalRUserApi];
            var path = context.Paths[TestPath.SignalRUserApi];
            var tick = Environment.TickCount;

            // Act.
            var connection = await context.CreateSignalRConnection($"https://localhost:{port}{path}/UserHub").ConfigureAwait(false);
            var result = await connection.InvokeCoreAsync("GetComplex", typeof(string), new object[]{ tick.ToString() } ).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result);
            Assert.Equal($"UserHub_{tick}", result);
        }
    }
}
