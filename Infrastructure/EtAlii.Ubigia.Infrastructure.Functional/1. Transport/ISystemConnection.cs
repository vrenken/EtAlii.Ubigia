namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public interface ISystemConnection : IDisposable
    { 
        Task<IDataConnection> OpenSpace(string accountName, string spaceName);

        Task<IManagementConnection> OpenManagementConnection();
    }
}
