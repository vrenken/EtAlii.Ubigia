// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Local
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Api.Grpc.WireProtocol;
    using EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.Grpc.WireProtocol;
    using Xunit;

    public class System2HostTestContextGrpcTests : IClassFixture<UnitTestContext<System2GrpcHostTestContext>>
    {
        private readonly UnitTestContext<System2GrpcHostTestContext> _context;

        public System2HostTestContextGrpcTests(UnitTestContext<System2GrpcHostTestContext> context)
        {
            _context = context;
        }

        [Fact]
        public async Task System2HostTestContextGrpc_User_Api_GetSimple()
        {
            // Arrange.
            var port = _context.Host.Ports[TestPort.GrpcUserApi];
            var path = _context.Host.Paths[TestPath.GrpcUserApi];

            // Act.
            var client = _context.Host.CreateClient($"https://localhost:{port}{path}", channel => new UserGrpcService.UserGrpcServiceClient(channel));
            var result = await client.GetSimpleAsync(new SimpleUserGetRequest());

            // Assert.
            Assert.NotNull(result);
            Assert.Equal("UserGrpcService", result.Result);
        }

        [Fact]
        public async Task System2HostTestContextGrpc_Admin_Api_GetSimple()
        {
            // Arrange.
            var port = _context.Host.Ports[TestPort.GrpcAdminApi];
            var path = _context.Host.Paths[TestPath.GrpcAdminApi];

            // Act.
            var client = _context.Host.CreateClient($"https://localhost:{port}{path}", channel => new AdminGrpcService.AdminGrpcServiceClient(channel));
            var result = await client.GetSimpleAsync(new SimpleAdminGetRequest());

            // Assert.
            Assert.NotNull(result);
            Assert.Equal("AdminGrpcService", result.Result);
        }

        [Fact]
        public async Task System2HostTestContextGrpc_User_Api_GetComplex()
        {
            // Arrange.
            var port = _context.Host.Ports[TestPort.GrpcUserApi];
            var path = _context.Host.Paths[TestPath.GrpcUserApi];
            var tick = Environment.TickCount;

            // Act.
            var client = _context.Host.CreateClient($"https://localhost:{port}{path}", channel => new UserGrpcService.UserGrpcServiceClient(channel));
            var result = await client.GetComplexAsync(new ComplexUserGetRequest { Postfix = tick.ToString() });

            // Assert.
            Assert.NotNull(result);
            Assert.Equal($"UserGrpcService_{tick}", result.Result);
        }

        [Fact]
        public async Task System2HostTestContextGrpc_Admin_Api_GetComplex()
        {
            // Arrange.
            var port = _context.Host.Ports[TestPort.GrpcAdminApi];
            var path = _context.Host.Paths[TestPath.GrpcAdminApi];
            var tick = Environment.TickCount;

            // Act.
            var client = _context.Host.CreateClient($"https://localhost:{port}{path}", channel => new AdminGrpcService.AdminGrpcServiceClient(channel));
            var result = await client.GetComplexAsync(new ComplexAdminGetRequest { Postfix = tick.ToString() });

            // Assert.
            Assert.NotNull(result);
            Assert.Equal($"AdminGrpcService_{tick}", result.Result);
        }
    }
}
