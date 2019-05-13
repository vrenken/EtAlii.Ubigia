namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;

    public class InfrastructureSystem : SystemBase, IInfrastructureSystem
    {
        private readonly ISystemCommandsFactory _systemCommandsFactory;

        public InfrastructureSystem(ISystemCommandsFactory systemCommandsFactory)
        {
            _systemCommandsFactory = systemCommandsFactory;
        }

        protected override Task Initialize(
	        IHost host, IService[] services, IModule[] modules, 
            out Status status, out ICommand[] commands)
        {
            status = new Status(nameof(InfrastructureSystem));
            commands = _systemCommandsFactory.Create(this);
            return Task.CompletedTask;
        }
    }
}