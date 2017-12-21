namespace EtAlii.Ubigia.Infrastructure.Transport.Owin
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Hosting.Owin;

    public class OwinInfrastructureExtension : IInfrastructureExtension
    {
        private readonly IApplicationManager _applicationManager;

        internal OwinInfrastructureExtension(IApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new OwinApplicationScaffolding(_applicationManager), 
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}