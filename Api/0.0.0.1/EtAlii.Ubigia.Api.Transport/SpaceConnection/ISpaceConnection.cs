namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface ISpaceConnection : IConnection, IDisposable
    {
        ISpaceConnectionConfiguration Configuration { get; }
        Space Space { get; }
        Account Account { get; } // TODO: Move to IConnection.

        IAuthenticationContext Authentication { get; }
        IEntryContext Entries { get; }
        IRootContext Roots { get; }
        IContentContext Content { get; }
        IPropertyContext Properties { get; }


    }

    public interface ISpaceConnection<out TTransport> : ISpaceConnection
        where TTransport: ISpaceTransport
    {

        TTransport Transport { get; }
    }
}
