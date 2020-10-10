namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
{
    public class InfrastructureSystem : SystemBase
    {
        private readonly ISystemCommandsFactory _systemCommandsFactory;

        public InfrastructureSystem(ISystemCommandsFactory systemCommandsFactory)
        {
            _systemCommandsFactory = systemCommandsFactory;
        }

        protected override ICommand[] CreateCommands() =>_systemCommandsFactory.Create(this); 
    }
}