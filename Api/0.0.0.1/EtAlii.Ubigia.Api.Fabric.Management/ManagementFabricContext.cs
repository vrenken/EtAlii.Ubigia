namespace EtAlii.Ubigia.Api.Management
{
    using EtAlii.Ubigia.Api.Transport;

    public class ManagementFabricContext : IManagementFabricContext
    {
        public Storage Storage => _storage;
        private readonly Storage _storage;

        public IStorageContext Storages => _storages;
        private readonly IStorageContext _storages;

        public IAccountContext Accounts => _accounts;
        private readonly IAccountContext _accounts;

        public ISpaceContext Spaces => _spaces;
        private readonly ISpaceContext _spaces;

        public ManagementFabricContext(
            Storage storage, 
            IStorageContext storages, 
            IAccountContext accounts, 
            ISpaceContext spaces)
        {
            _storage = storage;
            _storages = storages;
            _accounts = accounts;
            _spaces = spaces;
        }
    }
}
