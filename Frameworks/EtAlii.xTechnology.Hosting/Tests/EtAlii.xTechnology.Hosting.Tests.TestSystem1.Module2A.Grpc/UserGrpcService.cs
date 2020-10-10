﻿namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.Grpc.WireProtocol;
    using global::Grpc.Core;
    using WireUserGrpcService = global::EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.Grpc.WireProtocol.UserGrpcService;

    public class UserGrpcService : WireUserGrpcService.UserGrpcServiceBase
    {
        public override Task<UserGetResponse> GetSimple(SimpleUserGetRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UserGetResponse() { Result = nameof(UserGrpcService) });
        }

        public override Task<UserGetResponse> GetComplex(ComplexUserGetRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UserGetResponse() { Result = nameof(UserGrpcService) + "_" + request.Postfix });
        }
    }
}
