namespace EtAlii.Servus.Hosting.WindowsServiceHost
{
    using EtAlii.Servus.Hosting.Owin;
    using EtAlii.Servus.Infrastructure;

    public class WindowsServiceHostFactory : OwinSelfHostedHostFactory<WindowsServiceHost>
    {
        public WindowsServiceHostFactory(IHostConfiguration configuration) 
            : base(configuration)
        {
        }
    }
}
