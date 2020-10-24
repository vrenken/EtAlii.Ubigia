namespace EtAlii.xTechnology.Hosting
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class WindowsService : System.ServiceProcess.ServiceBase
    {
        private readonly IServiceLogic _serviceLogic;

        public WindowsService(IServiceLogic serviceLogic, ServiceDetails details)
        {
            _serviceLogic = serviceLogic;
            ServiceName = details.Name;
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