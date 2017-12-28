namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using EtAlii.xTechnology.Hosting;

    public class InfrastructureSystem : SystemBase<IAspNetCoreHost>, IInfrastructureSystem
    {
        private readonly ISystemCommandsFactory _systemCommandsFactory;

        public InfrastructureSystem(ISystemCommandsFactory systemCommandsFactory)
        {
            _systemCommandsFactory = systemCommandsFactory;
        }

        protected override void Initialize(
            IAspNetCoreHost host, IService[] services, IModule[] modules, 
            out Status status, out ICommand[] commands)
        {
            status = new Status(nameof(InfrastructureSystem));
            commands = _systemCommandsFactory.Create(this);
        }
    }
}