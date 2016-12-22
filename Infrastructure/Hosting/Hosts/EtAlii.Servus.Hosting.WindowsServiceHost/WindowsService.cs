namespace EtAlii.Servus.Infrastructure.Hosting.WindowsServiceHost
{
    using System.ServiceProcess;

    [System.ComponentModel.DesignerCategory("Code")]
    public class WindowsService : ServiceBase
    {
        private readonly IServiceLogic _serviceLogic;

        public WindowsService(IServiceLogic serviceLogic)
        {
            _serviceLogic = serviceLogic;
            ServiceName = serviceLogic.Name;
        }

        protected override void OnStart(string[] args)
        {
            _serviceLogic.Start(args);
        }

        protected override void OnStop()
        {
            _serviceLogic.Stop();
        }
    }
}