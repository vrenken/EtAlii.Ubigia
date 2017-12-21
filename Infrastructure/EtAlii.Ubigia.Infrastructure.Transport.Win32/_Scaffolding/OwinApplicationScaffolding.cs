namespace EtAlii.Ubigia.Infrastructure.Transport.Owin
{
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Hosting.Owin;

    internal class OwinApplicationScaffolding : IScaffolding
    {
        private readonly IApplicationManager _applicationManager;

        public OwinApplicationScaffolding(IApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }

        public void Register(Container container)
        {
            container.Register<IAuthenticationTokenConverter, AuthenticationTokenConverter>();

            if (_applicationManager != null)
            {
                container.Register(() => _applicationManager);
            }
            else
            {
                container.Register<IApplicationManager, DefaultApplicationManager>();
            }
        }
    }
}