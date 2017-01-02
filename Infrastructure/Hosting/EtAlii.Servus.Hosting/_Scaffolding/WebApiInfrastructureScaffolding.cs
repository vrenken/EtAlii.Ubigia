namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System.Web.Http;
    using EtAlii.Servus.Infrastructure;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    internal class WebApiInfrastructureScaffolding<TAuthenticationIdentityProvider> : IScaffolding
            where TAuthenticationIdentityProvider : class, IAuthenticationIdentityProvider
    {
        public void Register(Container container)
        {
            var webApiRequestTransientLifeStyle = new WebApiRequestLifestyle(true);
            container.Register<StorageController>(webApiRequestTransientLifeStyle);
            container.Register<SpaceController>(webApiRequestTransientLifeStyle);
            container.Register<AccountController>(webApiRequestTransientLifeStyle);

            container.Register<RootController>(webApiRequestTransientLifeStyle);
            container.Register<RootHubServerProxy>(webApiRequestTransientLifeStyle);

            container.Register<EntryController>(webApiRequestTransientLifeStyle);
            container.Register<EntryHubServerProxy>(webApiRequestTransientLifeStyle);

            container.Register<ContentController>(webApiRequestTransientLifeStyle);
            container.Register<ContentHubServerProxy>(webApiRequestTransientLifeStyle);

            container.Register<ContentDefinitionController>(webApiRequestTransientLifeStyle);
            container.Register<ContentDefinitionHubServerProxy>(webApiRequestTransientLifeStyle);


            container.Register<IAuthenticationIdentityProvider, TAuthenticationIdentityProvider>(Lifestyle.Singleton);
            container.Register<IAuthenticationVerifier, AuthenticationVerifier>(Lifestyle.Singleton);
            container.Register<IAuthenticationTokenVerifier, AuthenticationTokenVerifier>(Lifestyle.Singleton);

            container.Register<IComponentManager, ComponentManager>(Lifestyle.Transient);
            container.Register<HttpConfiguration>(() => CreateRegisterHttpConfiguration(container), Lifestyle.Transient);
        }

        private HttpConfiguration CreateRegisterHttpConfiguration(Container container)
        {
            var httpConfiguration = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };
            container.RegisterWebApiFilterProvider(httpConfiguration);
            return httpConfiguration;
        }
    }}