namespace EtAlii.Servus.Infrastructure.WebApi
{
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    internal class WebApiEntryScaffolding : IScaffolding
    {
        public WebApiEntryScaffolding()
        {
        }

        public void Register(Container container)
        {
            var webApiRequestTransientLifeStyle = new WebApiRequestLifestyle(true);
            container.Register<EntryController>(webApiRequestTransientLifeStyle);
            container.Register<EntryNotificationHubProxy>(webApiRequestTransientLifeStyle);
            container.Register<EntryDataHubProxy>(webApiRequestTransientLifeStyle);
        }
    }
}