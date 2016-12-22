namespace EtAlii.Servus.Api.Transport.WebApi
{
    using EtAlii.xTechnology.MicroContainer;

    public class WebApiSpaceTransport : SpaceTransportBase, IWebApiSpaceTransport
    {
        private readonly IInfrastructureClient _infrastructureClient;

        public WebApiSpaceTransport(IInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

        protected override IScaffolding[] CreateScaffoldingInternal()
        {
            return new IScaffolding[]
            {
                new WebApiSpaceClientsScaffolding(_infrastructureClient),
            };
        }
    }
}
