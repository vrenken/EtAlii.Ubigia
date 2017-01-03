namespace EtAlii.Ubigia.Infrastructure.WebApi
{
    using EtAlii.xTechnology.MicroContainer;

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
            container.Register<IAuthenticationIdentityProvider, TAuthenticationIdentityProvider>();
            container.Register<IAuthenticationVerifier, AuthenticationVerifier>();
            container.Register<IAuthenticationTokenVerifier, AuthenticationTokenVerifier>();
            container.Register<IAuthenticationTokenConverter, AuthenticationTokenConverter>();

            if (_applicationManager != null)
            {
                container.Register<IApplicationManager>(() => _applicationManager);
            }
            else
            {
                container.Register<IApplicationManager, DefaultApplicationManager>();
            }
        }
    }
}