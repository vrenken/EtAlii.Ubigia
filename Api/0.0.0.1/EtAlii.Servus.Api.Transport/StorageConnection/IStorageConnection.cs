namespace EtAlii.Servus.Api.Transport
{
    using System;

    public interface IStorageConnection : IConnection, IDisposable
    {
        IStorageContext Storages { get; }
        IAccountContext Accounts { get; }
        ISpaceContext Spaces { get; }

        IStorageConnectionConfiguration Configuration { get; }
    }

    public interface IStorageConnection<out TTransport> : IStorageConnection
        where TTransport: IStorageTransport
    {
        TTransport Transport { get; }
    }
}
