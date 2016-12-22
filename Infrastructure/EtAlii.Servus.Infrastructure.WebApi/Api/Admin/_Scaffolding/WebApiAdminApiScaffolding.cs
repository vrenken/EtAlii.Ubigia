namespace EtAlii.Servus.Infrastructure.WebApi
{
    using EtAlii.Servus.Infrastructure.Functional;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    internal class WebApiAdminApiScaffolding : IScaffolding
    {
        public WebApiAdminApiScaffolding()
        {
        }

        public void Register(Container container)
        {
            var webApiRequestTransientLifeStyle = new WebApiRequestLifestyle(true);
            container.Register<StorageController>(webApiRequestTransientLifeStyle);
            container.Register<SpaceController>(webApiRequestTransientLifeStyle);
            container.Register<AccountController>(webApiRequestTransientLifeStyle);

            container.Register<IWebApiAdminApiComponent, WebApiAdminApiComponent>(Lifestyle.Transient);
        }
    }
}