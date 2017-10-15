namespace EtAlii.Servus.Infrastructure.Transport
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Transport;

    public interface ISystemConnection : IDisposable
    { 
        Task<IDataConnection> OpenSpace(string accountName, string spaceName);

        Task<IManagementConnection> OpenManagementConnection();
    }
}
