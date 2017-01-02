namespace EtAlii.Servus.Api.Data
{
    public class HierarchicalContentManager : IHierarchicalContentManager
    {
        private readonly IDataConnection _connection;
        private readonly IContentManager _contentManager;

        public HierarchicalContentManager(IDataConnection connection, IContentManager contentManager)
        {
            _connection = connection;
            _contentManager = contentManager;
        }
    }
}
