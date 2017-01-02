namespace EtAlii.Servus.Infrastructure.Model
{
    using EtAlii.Servus.Storage;

    public abstract class RepositoryBase
    {
        protected IHostingConfiguration Configuration { get { return _configuration; } }
        private readonly IHostingConfiguration _configuration;

        protected IItemStorage ItemStorage { get { return _itemStorage; } }
        private readonly IItemStorage _itemStorage;

        public RepositoryBase(IItemStorage itemStorage, IHostingConfiguration configuration)
        {
            _itemStorage = itemStorage;
            _configuration = configuration;
        }
    }
}