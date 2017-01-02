namespace EtAlii.Servus.Api.Tests
{
    using EtAlii.Servus.Infrastructure.Hosts;
    using EtAlii.Servus.Infrastructure.Model;
    using EtAlii.Servus.Infrastructure.Model.Tests;
    using SimpleInjector;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class InfrastructureApp : EtAlii.Servus.Infrastructure.App
    {
        protected override void RegisterTypes()
        {
            base.RegisterTypes();
         
            Container.Register<WebApiInMemoryHost, WebApiInMemoryHost>(Lifestyle.Singleton);
        }

        protected override IHostingConfiguration GetConfiguration()
        {
            var configuration = new Configuration
            {
                Name = "Unit test storage - Api",
                Address = "http://localhost:62000",
                Account = "unittest",
                Password = "123",
            };
            return configuration;
        }
    }
}
