namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface IStorageTransportProvider : ITransportProvider
    {
        IStorageTransport GetStorageTransport(Uri address);
    }
}