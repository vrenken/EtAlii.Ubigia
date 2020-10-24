namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class WebApiSpaceTransport : SpaceTransportBase, IWebApiSpaceTransport
    {
        private readonly IInfrastructureClient _infrastructureClient;

        public WebApiSpaceTransport(Uri address, IInfrastructureClient infrastructureClient)
            : base(address)
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
