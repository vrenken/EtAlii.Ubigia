namespace EtAlii.xTechnology.Hosting.Tests.Provisioning.NetCore
{
    public class ProvisioningSystem : SystemBase
    {
        private readonly ISystemCommandsFactory _systemCommandsFactory;

        public ProvisioningSystem(ISystemCommandsFactory systemCommandsFactory)
        {
            _systemCommandsFactory = systemCommandsFactory;
        }

        protected override ICommand[] CreateCommands() =>_systemCommandsFactory.Create(this); 
    }
}