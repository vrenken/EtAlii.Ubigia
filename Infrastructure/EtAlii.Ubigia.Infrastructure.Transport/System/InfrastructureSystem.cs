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

        //protected override Status CreateInitialStatus() => new Status(GetType().Name);

        protected override ICommand[] CreateCommands() => _systemCommandsFactory.Create(this);

        // protected override Task Initialize(
        //     IHost host, IService[] services, IModule[] modules, 
        //     out Status status, out ICommand[] commands)
        // {
        //     status = new Status(nameof(InfrastructureSystem));
        //     commands = _systemCommandsFactory.Create(this);
        //     return Task.CompletedTask;
        // }
    } 
}