namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    public interface IManagementConnection : IDisposable
    {
        bool IsConnected { get; }
        IManagementConnectionConfiguration Configuration { get; }

        Storage Storage { get; }
        IStorageContext Storages { get; }
        IAccountContext Accounts { get; }
        ISpaceContext Spaces { get; }

        Task<IDataConnection> OpenSpace(Space space);
        Task<IDataConnection> OpenSpace(Guid accountId, Guid spaceId);
        Task<IDataConnection> OpenSpace(string accountName, string spaceName);

        Task Open();
        Task Close();
    }
}
