namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;

    public class HostCommandsFactory
    {
        public HostCommandsFactory()
        {
        }

        public IHostCommand[] Create()
        {
            var container = new Container();

            container.Register<IStartHostCommand, StartHostCommand>();
            container.Register<IStopHostCommand, StopHostCommand>();
            container.Register<IShutdownHostCommand, ShutdownHostCommand>();
            
            return new IHostCommand[]
            {
                container.GetInstance<IStartHostCommand>(),
                container.GetInstance<IStopHostCommand>(),
                container.GetInstance<IShutdownHostCommand>(),
            };
        }
    }
}
