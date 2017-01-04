namespace EtAlii.Ubigia.Infrastructure.WebApi
{
    using System.Linq;
    using System.Web.Http;
    using EtAlii.xTechnology.MicroContainer;

    public partial class WebApiComponentManagerFactory
    {
        public IWebApiComponentManager Create(Container container, object[] components)
        {
            var httpConfiguration = new HttpConfiguration
            {
                DependencyResolver = new MicroContainerDependencyResolver(container)
            };

            var webApiComponentManager = new WebApiComponentManager(httpConfiguration, components.OfType<IComponent>().ToArray());
            return webApiComponentManager;
        }
    }
}