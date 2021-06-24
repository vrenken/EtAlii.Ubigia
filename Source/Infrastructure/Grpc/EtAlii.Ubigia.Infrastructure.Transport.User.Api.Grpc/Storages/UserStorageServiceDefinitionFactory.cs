// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;
    using global::Grpc.Core.Interceptors;

    public class UserStorageServiceDefinitionFactory : IUserStorageServiceDefinitionFactory
    {
        public ServerServiceDefinition Create(IInfrastructure infrastructure, IAccountAuthenticationInterceptor accountAuthenticationInterceptor)
        {
            var container = new Container();
            container.Register<IUserStorageService, UserStorageService>();
       
            new UserApiScaffolding(infrastructure).Register(container);
            new AuthenticationScaffolding().Register(container);     
            new SerializationScaffolding().Register(container);
            
            var storageService = (UserStorageService)container.GetInstance<IUserStorageService>();
            var serverServiceDefinition = StorageGrpcService.BindService(storageService);
            return serverServiceDefinition.Intercept((Interceptor)accountAuthenticationInterceptor);
        }
    }
}