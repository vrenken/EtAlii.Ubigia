namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;

    public class UserEntryServiceDefinitionFactory : IUserEntryServiceDefinitionFactory
    {
        public ServerServiceDefinition Create(IInfrastructure infrastructure)
        {
            var container = new Container();
            container.Register<IUserEntryService, UserEntryService>();
       
            new AuthenticationScaffolding(infrastructure).Register(container);     
            new SerializationScaffolding().Register(container);
            
            var entryService = (UserEntryService)container.GetInstance<IUserEntryService>();
            return EntryGrpcService.BindService(entryService);
        }
    }
}