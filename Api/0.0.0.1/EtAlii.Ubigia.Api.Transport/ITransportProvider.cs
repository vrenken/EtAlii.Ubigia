namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface ITransportProvider
    {
        ISpaceTransport GetSpaceTransport(Uri address);
    }
}