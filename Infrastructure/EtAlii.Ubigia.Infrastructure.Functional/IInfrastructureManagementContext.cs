namespace EtAlii.Ubigia.Infrastructure.Functional
{
    public interface IInfrastructureManagementContext
    {
        IStorageRepository Storages { get; }
        ISpaceRepository Spaces { get; }
        IAccountRepository Accounts { get; }
    }
}