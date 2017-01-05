namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using EtAlii.xTechnology.MicroContainer;

    public partial class WebApiComponentManagerFactory
    {
        public IWebApiComponentManager Create(Container container, Func<Container, object>[] componentFactories)
        {
            var typedComponents = componentFactories
                .Select(componentFactory => componentFactory(container))
                .OfType<IWebApiComponent>()
                .ToArray();

            var httpConfiguration = new HttpConfiguration
            {
                DependencyResolver = new MicroContainerDependencyResolver(container, typedComponents)
            };

            var webApiComponentManager = new WebApiComponentManager(httpConfiguration, typedComponents);
            return webApiComponentManager;
        }
    }
}