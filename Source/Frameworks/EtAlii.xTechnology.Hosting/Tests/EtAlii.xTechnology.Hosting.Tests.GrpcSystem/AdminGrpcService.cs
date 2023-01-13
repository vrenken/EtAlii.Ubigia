// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.GrpcSystem;

using System.Threading.Tasks;
using EtAlii.xTechnology.Hosting.Tests.GrpcSystem.WireProtocol;
using Grpc.Core;
using WireAdminGrpcService = global::EtAlii.xTechnology.Hosting.Tests.GrpcSystem.WireProtocol.AdminGrpcService;

public class AdminGrpcService : WireAdminGrpcService.AdminGrpcServiceBase
{
    public override Task<AdminGetResponse> GetSimple(SimpleAdminGetRequest request, ServerCallContext context)
    {
        return Task.FromResult(new AdminGetResponse { Result = nameof(AdminGrpcService) });
    }

    public override Task<AdminGetResponse> GetComplex(ComplexAdminGetRequest request, ServerCallContext context)
    {
        return Task.FromResult(new AdminGetResponse { Result = nameof(AdminGrpcService) + "_" + request.Postfix });
    }
}
