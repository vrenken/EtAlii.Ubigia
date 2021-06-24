// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.GrpcSystem
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting.Tests.GrpcSystem.WireProtocol;
    using Grpc.Core;
    using WireUserGrpcService = global::EtAlii.xTechnology.Hosting.Tests.GrpcSystem.WireProtocol.UserGrpcService;

    public class UserGrpcService : WireUserGrpcService.UserGrpcServiceBase
    {
        public override Task<UserGetResponse> GetSimple(SimpleUserGetRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UserGetResponse { Result = nameof(UserGrpcService) });
        }

        public override Task<UserGetResponse> GetComplex(ComplexUserGetRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UserGetResponse { Result = nameof(UserGrpcService) + "_" + request.Postfix });
        }
    }
}
