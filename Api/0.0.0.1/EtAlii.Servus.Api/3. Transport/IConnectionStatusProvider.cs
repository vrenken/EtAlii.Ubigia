namespace EtAlii.Servus.Api.Transport
{
    using System;

    public interface IConnectionStatusProvider
    {
        bool IsConnected { get; }
        TimeSpan Duration { get; }
        void Initialize(IDataConnection connection);
    }
}
