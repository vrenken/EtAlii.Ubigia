namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.MicroContainer;

    public partial class LocalContentDataClient : LocalDataClientBase<IDataConnection>, IContentDataClient
    {
        public LocalContentDataClient(Container container)
            : base(container)
        {
        }
    }
}
