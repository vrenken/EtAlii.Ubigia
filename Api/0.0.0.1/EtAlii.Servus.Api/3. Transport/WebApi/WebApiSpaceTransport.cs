namespace EtAlii.Servus.Api.Transport.WebApi
{
    using EtAlii.xTechnology.MicroContainer;

    public class WebApiSpaceTransport : SpaceTransportBase, IWebApiSpaceTransport
    {
        protected override IScaffolding[] CreateScaffoldingInternal()
        {
            return new IScaffolding[]
            {
                new WebApiSpaceClientsScaffolding(),
            };
        }
    }
}
