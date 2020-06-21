namespace EtAlii.Ubigia.PowerShell
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.PowerShell.Accounts;
    using EtAlii.Ubigia.PowerShell.Entries;
    using EtAlii.Ubigia.PowerShell.Roots;
    using EtAlii.Ubigia.PowerShell.Spaces;
    using EtAlii.Ubigia.PowerShell.Storages;

    public interface IPowerShellClient
    {
        IStorageResolver StorageResolver { get; }
        IEntryResolver EntryResolver { get; }
        ISpaceResolver SpaceResolver { get; }
        IAccountResolver AccountResolver { get; }
        IRootResolver RootResolver { get; }

        IManagementConnection ManagementConnection { get; }
        IFabricContext Fabric { get; set; }
        IInfrastructureClient Client { get; }

        Task OpenManagementConnection(Uri address, string accountName, string password);
    }
}
