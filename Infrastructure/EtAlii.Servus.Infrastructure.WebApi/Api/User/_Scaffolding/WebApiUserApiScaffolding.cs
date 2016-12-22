namespace EtAlii.Servus.Infrastructure.WebApi
{
    using EtAlii.Servus.Infrastructure.Functional;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    internal class WebApiUserApiScaffolding : IScaffolding
    {
        public WebApiUserApiScaffolding()
        {
        }

        public void Register(Container container)
        {
            var webApiRequestTransientLifeStyle = new WebApiRequestLifestyle(true);
            container.Register<EntryController>(webApiRequestTransientLifeStyle);
            container.Register<ContentController>(webApiRequestTransientLifeStyle);
            container.Register<ContentDefinitionController>(webApiRequestTransientLifeStyle);
            container.Register<PropertiesController>(webApiRequestTransientLifeStyle);
            container.Register<RootController>(webApiRequestTransientLifeStyle);

            container.Register<IWebApiUserApiComponent, WebApiUserApiComponent>(Lifestyle.Transient);
        }
    }
}