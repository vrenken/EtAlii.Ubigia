namespace EtAlii.Servus.PowerShell.Tests
{
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Hosting;

    public class PowerShellTestHostFactory : HostFactoryBase<PowerShellHost>
    {
        public PowerShellTestHostFactory(IHostConfiguration configuration) 
            : base(configuration)
        {
        }
    }
}
