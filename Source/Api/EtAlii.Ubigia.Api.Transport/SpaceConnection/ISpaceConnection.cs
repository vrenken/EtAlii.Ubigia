// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface ISpaceConnection : IConnection, IDisposable
    {
        ISpaceTransport Transport { get; }
        /// <summary>
        /// The Configuration used to instantiate this SpaceConnection.
        /// </summary>
        ISpaceConnectionConfiguration Configuration { get; }
        Space Space { get; }
        Account Account { get; } // TODO: Move to IConnection.

        IAuthenticationContext Authentication { get; }
        IEntryContext Entries { get; }
        IRootContext Roots { get; }
        IContentContext Content { get; }
        IPropertiesContext Properties { get; }
    }

    public interface ISpaceConnection<out TTransport> : ISpaceConnection
        where TTransport: ISpaceTransport
    {
       new TTransport Transport { get; }
    }
}
