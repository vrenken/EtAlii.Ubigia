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
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantSignalR);
            await context.Start();
            var port = context.Ports[TestPort.SignalRUserApi];
            var path = context.Paths[TestPath.SignalRUserApi];

            // Act.
            var connection = await context.CreateSignalRConnection($"http://localhost:{port}{path}/UserHub");
            var result = await connection.InvokeCoreAsync("GetSimple", typeof(string), new object[]{} );
            
            // Assert.
            Assert.NotNull(result);
            Assert.StartsWith("UserHub_", (result as string)!);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetIncorrect_01()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantSignalR);
            await context.Start();
            var port = context.Ports[TestPort.SignalRUserApi];
            var path = context.Paths[TestPath.SignalRUserApi];

            // Act.
            var act = context.CreateSignalRConnection($"http://localhost:{port}{path}/AdminHub");
            
            // Assert.
            await Assert.ThrowsAsync<HttpRequestException>(async () => await act);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetIncorrect_02()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantSignalR);
            await context.Start();
            var port = context.Ports[TestPort.SignalRUserApi];
            var path = context.Paths[TestPath.SignalRUserApi];

            // Act.
            var act = context.CreateSignalRConnection($"http://localhost:{port}{path}/admin/api/UserHub");
            
            // Assert.
            await Assert.ThrowsAsync<HttpRequestException>(async () => await act);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_Admin_Api_GetSimple()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantSignalR);
            await context.Start();
            var port = context.Ports[TestPort.SignalRAdminApi];
            var path = context.Paths[TestPath.SignalRAdminApi];

            // Act.
            var connection = await context.CreateSignalRConnection($"http://localhost:{port}{path}/AdminHub");
            var result = await connection.InvokeCoreAsync("GetSimple", typeof(string), new object[]{} );
            
            // Assert.
            Assert.NotNull(result);
            Assert.StartsWith("AdminHub_", (result as string)!);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_Admin_Api_GetIncorrect_01()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantSignalR);
            await context.Start();
            var port = context.Ports[TestPort.SignalRAdminApi];
            var path = context.Paths[TestPath.SignalRAdminApi];

            // Act.
            var act = context.CreateSignalRConnection($"http://localhost:{port}{path}/UserHub");
            
            // Assert.
            await Assert.ThrowsAsync<HttpRequestException>(async () => await act);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_Admin_Api_GetIncorrect_02()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantSignalR);
            await context.Start();
            var port = context.Ports[TestPort.SignalRAdminApi];
            var path = context.Paths[TestPath.SignalRAdminApi];

            // Act.
            var act = context.CreateSignalRConnection($"http://localhost:{port}{path}/user/api/AdminHub");
            
            // Assert.
            await Assert.ThrowsAsync<HttpRequestException>(async () => await act);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_User_Api_GetComplex()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantSignalR);
            await context.Start();
            var port = context.Ports[TestPort.SignalRUserApi];
            var path = context.Paths[TestPath.SignalRUserApi];
            var tick = Environment.TickCount;

            // Act.
            var connection = await context.CreateSignalRConnection($"http://localhost:{port}{path}/UserHub");
            var result = await connection.InvokeCoreAsync("GetComplex", typeof(string), new object[]{ tick.ToString() } );
            
            // Assert.
            Assert.NotNull(result);
            Assert.Equal($"UserHub_{tick}", result);
        }

        [Fact]
        public async Task System2HostTestContextSignalR_Admin_Api_GetComplex()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantSignalR);
            await context.Start();
            var port = context.Ports[TestPort.SignalRAdminApi];
            var path = context.Paths[TestPath.SignalRAdminApi];
            var tick = Environment.TickCount;

            // Act.
            var connection = await context.CreateSignalRConnection($"http://127.0.0.1:{port}{path}/AdminHub");
            var result = await connection.InvokeCoreAsync("GetComplex", typeof(string), new object[]{ tick.ToString() } );
            
            // Assert.
            Assert.NotNull(result);
            Assert.Equal($"AdminHub_{tick}", result);
        }
    }
}
