namespace EtAlii.xTechnology.Hosting
{
    using System.Web;

    public class WebApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Start logging.
            //Logger.StartSession(); // Disabled because of performance loss.

            //var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.Infrastructure");

            //var hostFactory = (IHostFactory)WebConfigurationManager.GetWebApplicationSection("hosting");
            //var infrastructureFactory = (IInfrastructureFactory)WebConfigurationManager.GetWebApplicationSection("infrastructure");
            //var storageFactory = (IStorageFactory)WebConfigurationManager.GetWebApplicationSection("storage");

            //var host = hostFactory.Create(infrastructureFactory, storageFactory, diagnostics);
            //host.Start();

            //AreaRegistration.RegisterAllAreas();
            // Add these two lines to initialize Routes and Filters:

            //var httpConfiguration = GlobalConfiguration.Configuration;

        }
    }
}