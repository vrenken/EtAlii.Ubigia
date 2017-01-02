namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.MicroContainer;

    public partial class WebApiContentDataClient : WebApiDataClientBase<IDataConnection>, IContentDataClient
    {
        public WebApiContentDataClient(Container container, IAddressFactory addressFactory, IInfrastructureClient client)
            : base(container, addressFactory, client)
        {
        }
    }
}
