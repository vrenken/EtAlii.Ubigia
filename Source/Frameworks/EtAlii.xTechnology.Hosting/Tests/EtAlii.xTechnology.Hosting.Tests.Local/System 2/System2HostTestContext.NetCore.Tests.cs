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
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantRest);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.RestUserApi];
            var path = context.Paths[TestPath.RestUserApi];

            // Act.
            using var client = context.CreateClient();
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
            var context = new HostTestContext(ConfigurationFiles.Systems2VariantRest);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.RestUserApi];
            var path = context.Paths[TestPath.RestUserApi];
            var tick = Environment.TickCount;

            // Act.
            using var client = context.CreateClient();
            var result = await client.GetAsync($"http://localhost:{port}{path}/data/GetComplex?postfix={tick}").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK,result.StatusCode);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.StartsWith($"UserController_{tick}", content);
        }
    }
}
