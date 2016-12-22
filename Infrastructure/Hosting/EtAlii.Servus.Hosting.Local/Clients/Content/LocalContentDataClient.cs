namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.MicroContainer;

    public partial class LocalContentDataClient : LocalDataClientBase<IDataConnection>, IContentDataClient
    {
        public LocalContentDataClient(Container container)
            : base(container)
        {
        }
    }
}
