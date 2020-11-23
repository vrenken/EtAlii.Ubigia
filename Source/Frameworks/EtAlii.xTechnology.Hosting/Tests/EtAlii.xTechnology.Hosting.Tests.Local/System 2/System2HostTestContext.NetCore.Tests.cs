namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public class System2HostTestContextNetCoreTests
    {
        [Fact]
        public async Task System2HostTestContextNetCore_User_Api_Get_1()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantWebApi);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.RestUserApi];
            var path = context.Paths[TestPath.RestUserApi];

            // Act.
            var client = context.CreateClient();
            var result = await client.GetAsync($"http://localhost:{port}{path}/data").ConfigureAwait(false);
            
            // Assert.
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK,result.StatusCode);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.StartsWith("UserController_", content);
        }

        [Fact]
        public async Task System2HostTestContextNetCore_User_Api_Get_2()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantWebApi);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.RestUserApi];
            var path = context.Paths[TestPath.RestUserApi];
            var tick = Environment.TickCount;

            // Act.
            var client = context.CreateClient();
            var result = await client.GetAsync($"http://localhost:{port}{path}/data/GetComplex?postfix={tick}").ConfigureAwait(false);
            
            // Assert.
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK,result.StatusCode);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.StartsWith($"UserController_{tick}", content);
        }

        [Fact]
        public async Task System2HostTestContextNetCore_User_Api_Get_Incorrect()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantWebApi);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.RestAdminApi];
            var path = context.Paths[TestPath.RestUserApi];

            // Act.
            var client = context.CreateClient();
            var result = await client.GetAsync($"http://localhost:{port}{path}").ConfigureAwait(false);
            
            // Assert.
            Assert.NotNull(result);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal("", content);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task System2HostTestContextNetCore_Admin_Api_Get_1()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantWebApi);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.RestAdminApi];
            var path = context.Paths[TestPath.RestAdminApi];
            
            // Act.
            var client = context.CreateClient();
            var result = await client.GetAsync($"http://localhost:{port}{path}/data").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK,result.StatusCode);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.StartsWith("AdminController_", content);
        }

        [Fact]
        public async Task System2HostTestContextNetCore_Admin_Api_Get_2()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantWebApi);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.RestAdminApi];
            var path = context.Paths[TestPath.RestAdminApi];
            var tick = Environment.TickCount;

            // Act.
            var client = context.CreateClient();
            var result = await client.GetAsync($"http://localhost:{port}{path}/data/GetComplex?postfix={tick}").ConfigureAwait(false);
            
            // Assert.
            Assert.NotNull(result);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal($"AdminController_{tick}", content);
        }


        [Fact]
        public async Task System2HostTestContextNetCore_Admin_Api_Get_Incorrect_01()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantWebApi);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.RestUserApi];
            var path = context.Paths[TestPath.RestAdminApi];

            // Act.
            var client = context.CreateClient();
            var result = await client.GetAsync($"http://localhost:{port}{path}/data").ConfigureAwait(false);
            
            // Assert.
            Assert.NotNull(result);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal("", content);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task System2HostTestContextNetCore_Admin_Api_Get_Incorrect_02()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantWebApi);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.RestUserApi];
            var path = context.Paths[TestPath.RestAdminApi] + "/bad";

            // Act.
            var client = context.CreateClient();
            var result = await client.GetAsync($"http://localhost:{port}{path}/data").ConfigureAwait(false);
            
            // Assert.
            Assert.NotNull(result);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal("", content);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
