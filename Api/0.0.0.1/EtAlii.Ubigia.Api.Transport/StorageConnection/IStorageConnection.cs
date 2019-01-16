namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface IStorageConnection : IConnection, IDisposable
    {
        IStorageTransport Transport { get; }
        IStorageContext Storages { get; }
        IAccountContext Accounts { get; }
        ISpaceContext Spaces { get; }

        IStorageConnectionConfiguration Configuration { get; }
    }

    public interface IStorageConnection<out TTransport> : IStorageConnection
        where TTransport: IStorageTransport
    {
        new TTransport Transport { get; }
    }
}
