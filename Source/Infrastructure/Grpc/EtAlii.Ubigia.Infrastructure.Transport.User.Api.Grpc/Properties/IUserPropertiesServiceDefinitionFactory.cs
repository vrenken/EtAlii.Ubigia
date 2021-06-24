// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public interface IUserPropertiesServiceDefinitionFactory
    {
        ServerServiceDefinition Create(IInfrastructure infrastructure, ISpaceAuthenticationInterceptor spaceAuthenticationInterceptor);
    }
}
