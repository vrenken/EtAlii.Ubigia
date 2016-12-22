namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using EtAlii.Servus.Api.Transport.WebApi;
    using EtAlii.Servus.Infrastructure.Functional;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    internal class WebApiApiScaffolding<TAuthenticationIdentityProvider> : IScaffolding
            where TAuthenticationIdentityProvider : class, IAuthenticationIdentityProvider
    {
        private readonly IApplicationManager _applicationManager;

        public WebApiApiScaffolding()
        {
        }

        public WebApiApiScaffolding(IApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }

        public void Register(Container container)
        {
            container.Register<IAuthenticationIdentityProvider, TAuthenticationIdentityProvider>(Lifestyle.Singleton);
            container.Register<IAuthenticationVerifier, AuthenticationVerifier>(Lifestyle.Singleton);
            container.Register<IAuthenticationTokenVerifier, AuthenticationTokenVerifier>(Lifestyle.Singleton);
            container.Register<IAuthenticationTokenConverter, AuthenticationTokenConverter>(Lifestyle.Singleton);

            if (_applicationManager != null)
            {
                container.Register<IApplicationManager>(() => _applicationManager, Lifestyle.Singleton);
            }
            else
            {
                container.Register<IApplicationManager, DefaultApplicationManager>(Lifestyle.Singleton);
            }

            container.Register<MediaTypeFormatter, PayloadMediaTypeFormatter>(Lifestyle.Singleton);

            container.Register<IWebApiComponentManager, WebApiComponentManager>(Lifestyle.Transient);

            container.Register<HttpConfiguration>(() => CreateRegisterHttpConfiguration(container), Lifestyle.Transient);
        }

        private HttpConfiguration CreateRegisterHttpConfiguration(Container container)
        {
            var httpConfiguration = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };
            return httpConfiguration;
        }
    }}