// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc;

using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
using EtAlii.xTechnology.MicroContainer;
using global::Grpc.Core;
using global::Grpc.Core.Interceptors;

// Referred to in the configuration Json.
public class UserSpaceServiceDefinitionFactory : IUserSpaceServiceDefinitionFactory
{
    public ServerServiceDefinition Create(IFunctionalContext functionalContext, ISpaceAuthenticationInterceptor spaceAuthenticationInterceptor)
    {
        var container = new Container();
        container.Register<IUserSpaceService, UserSpaceService>();

        new UserApiScaffolding(functionalContext).Register(container);
        new AuthenticationScaffolding().Register(container);
        new SerializationScaffolding().Register(container);

        var spaceService = (UserSpaceService)container.GetInstance<IUserSpaceService>();
        var serverServiceDefinition = SpaceGrpcService.BindService(spaceService);
        return serverServiceDefinition.Intercept((Interceptor)spaceAuthenticationInterceptor);
    }
}
