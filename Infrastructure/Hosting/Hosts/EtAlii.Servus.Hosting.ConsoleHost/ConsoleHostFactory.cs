namespace EtAlii.Servus.Hosting.ConsoleHost
{
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Hosting.Owin;

    public class ConsoleHostFactory : OwinSelfHostedHostFactory<ConsoleHost>
    {
        public ConsoleHostFactory(IHostConfiguration configuration) 
            : base(configuration)
        {
        }
    }
}
