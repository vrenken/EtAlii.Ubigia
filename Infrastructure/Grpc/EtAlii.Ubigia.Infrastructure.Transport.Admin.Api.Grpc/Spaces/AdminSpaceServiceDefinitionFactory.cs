﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc.Spaces
{
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;

    public class AdminSpaceServiceDefinitionFactory : IAdminSpaceServiceDefinitionFactory
    {
        public ServerServiceDefinition Create(IInfrastructure infrastructure)
        {
            var container = new Container();
            container.Register<IAdminSpaceService, AdminSpaceService>();
       
            new AdminApiScaffolding(infrastructure).Register(container);
            new AuthenticationScaffolding().Register(container);     
            new SerializationScaffolding().Register(container);
            
            var spaceService = (AdminSpaceService)container.GetInstance<IAdminSpaceService>();
            return SpaceGrpcService.BindService(spaceService);
        }
    }
}