namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using EtAlii.xTechnology.MicroContainer;

    internal class WebApiApiScaffolding<TAuthenticationIdentityProvider> : IScaffolding
            where TAuthenticationIdentityProvider : class, IAuthenticationIdentityProvider
    {
        public void Register(Container container)
        {
            container.Register<IAuthenticationIdentityProvider, TAuthenticationIdentityProvider>();
            container.Register<IAuthenticationVerifier, AuthenticationVerifier>();
            container.Register<IAuthenticationTokenVerifier, AuthenticationTokenVerifier>();
        }
    }
}