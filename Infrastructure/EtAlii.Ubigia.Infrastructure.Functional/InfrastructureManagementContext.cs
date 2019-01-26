namespace EtAlii.Ubigia.Infrastructure.Functional
{
    public class InfrastructureManagementContext : IInfrastructureManagementContext
    {

        public ISpaceRepository Spaces { get; }
        
        public IAccountRepository Accounts { get; }
        
        public IStorageRepository Storages { get; }

        protected InfrastructureManagementContext(
            ISpaceRepository spaces,
            IAccountRepository accounts,
            IStorageRepository storages)
        {
            Spaces = spaces;
            Accounts = accounts;
            Storages = storages;
        }
    }
}