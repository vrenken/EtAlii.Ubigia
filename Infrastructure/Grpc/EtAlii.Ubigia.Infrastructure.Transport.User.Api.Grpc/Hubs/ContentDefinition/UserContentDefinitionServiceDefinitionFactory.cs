﻿namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;

    public class UserContentDefinitionServiceDefinitionFactory : IUserContentDefinitionServiceDefinitionFactory
    {
        public ServerServiceDefinition Create(IInfrastructure infrastructure)
        {
            var container = new Container();
            container.Register<IUserContentDefinitionService, UserContentDefinitionService>();
       
            new AuthenticationScaffolding(infrastructure).Register(container);     
            new SerializationScaffolding().Register(container);
            
            var contentDefinitionService = (UserContentDefinitionService)container.GetInstance<IUserContentDefinitionService>();
            return ContentDefinitionGrpcService.BindService(contentDefinitionService);
        }
    }
}