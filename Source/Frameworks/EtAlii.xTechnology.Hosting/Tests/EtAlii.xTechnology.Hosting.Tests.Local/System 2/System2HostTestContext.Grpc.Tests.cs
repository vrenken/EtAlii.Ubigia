// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Api.Grpc.WireProtocol;
    using EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.Grpc.WireProtocol;
    using Xunit;

    public class System2HostTestContextGrpcTests
    {
        [Fact]
        public async Task System2HostTestContextGrpc_User_Api_GetSimple()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.HostSettingsSystems2VariantGrpc, ConfigurationFiles.ClientSettings);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.GrpcUserApi];
            var path = context.Paths[TestPath.GrpcUserApi];

            // Act.
            var client = context.CreateClient($"https://localhost:{port}{path}", channel => new UserGrpcService.UserGrpcServiceClient(channel));
            var result = await client.GetSimpleAsync(new SimpleUserGetRequest());

            // Assert.
            Assert.NotNull(result);
            Assert.Equal("UserGrpcService", result.Result);
        }

        [Fact]
        public async Task System2HostTestContextGrpc_Admin_Api_GetSimple()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.HostSettingsSystems2VariantGrpc, ConfigurationFiles.ClientSettings);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.GrpcAdminApi];
            var path = context.Paths[TestPath.GrpcAdminApi];

            // Act.
            var client = context.CreateClient($"https://localhost:{port}{path}", channel => new AdminGrpcService.AdminGrpcServiceClient(channel));
            var result = await client.GetSimpleAsync(new SimpleAdminGetRequest());

            // Assert.
            Assert.NotNull(result);
            Assert.Equal("AdminGrpcService", result.Result);
        }

        [Fact]
        public async Task System2HostTestContextGrpc_User_Api_GetComplex()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.HostSettingsSystems2VariantGrpc, ConfigurationFiles.ClientSettings);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.GrpcUserApi];
            var path = context.Paths[TestPath.GrpcUserApi];
            var tick = Environment.TickCount;

            // Act.
            var client = context.CreateClient($"https://localhost:{port}{path}", channel => new UserGrpcService.UserGrpcServiceClient(channel));
            var result = await client.GetComplexAsync(new ComplexUserGetRequest { Postfix = tick.ToString()});

            // Assert.
            Assert.NotNull(result);
            Assert.Equal($"UserGrpcService_{tick}", result.Result);
        }

        [Fact]
        public async Task System2HostTestContextGrpc_Admin_Api_GetComplex()
        {
            // Arrange.
            var context = new HostTestContext(ConfigurationFiles.HostSettingsSystems2VariantGrpc, ConfigurationFiles.ClientSettings);
            await context.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
            var port = context.Ports[TestPort.GrpcAdminApi];
            var path = context.Paths[TestPath.GrpcAdminApi];
            var tick = Environment.TickCount;

            // Act.
            var client = context.CreateClient($"https://localhost:{port}{path}", channel => new AdminGrpcService.AdminGrpcServiceClient(channel));
            var result = await client.GetComplexAsync(new ComplexAdminGetRequest { Postfix = tick.ToString()});

            // Assert.
            Assert.NotNull(result);
            Assert.Equal($"AdminGrpcService_{tick}", result.Result);
        }
    }
}
