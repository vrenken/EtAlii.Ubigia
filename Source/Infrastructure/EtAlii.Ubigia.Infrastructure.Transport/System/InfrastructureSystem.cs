namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.xTechnology.Hosting;

    public class InfrastructureSystem : SystemBase, IInfrastructureSystem
    {
        private readonly ISystemCommandsFactory _systemCommandsFactory;

        public InfrastructureSystem(ISystemCommandsFactory systemCommandsFactory)
        {
            _systemCommandsFactory = systemCommandsFactory;
        }

        protected override ICommand[] CreateCommands() => _systemCommandsFactory.Create(this);
    } 
}