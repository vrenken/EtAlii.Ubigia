namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    public interface ISpaceConnection : IConnection, IDisposable
    {
        ISpaceConnectionConfiguration Configuration { get; }
        Storage Storage { get; }
        Space Space { get; }
        Account Account { get; } // TODO: Move to IConnection.
    }
}
