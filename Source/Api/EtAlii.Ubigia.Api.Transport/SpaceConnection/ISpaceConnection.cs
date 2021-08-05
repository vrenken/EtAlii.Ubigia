// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface ISpaceConnection : IConnection, IDisposable
    {
        ISpaceTransport Transport { get; }

        /// <summary>
        /// The Options used to instantiate this SpaceConnection.
        /// </summary>
        ISpaceConnectionOptions Options { get; }

        /// <summary>
        /// The space that is accessed.
        /// </summary>
        Space Space { get; }

        IAuthenticationContext Authentication { get; }

        /// <summary>
        /// Provides access to the entries stored in the space.
        /// </summary>
        IEntryContext Entries { get; }

        /// <summary>
        /// Provides access to the roots through which the entries in the space can be found.
        /// </summary>
        IRootContext Roots { get; }

        /// <summary>
        /// Provides access to the content as stored in the entries in the space.
        /// </summary>
        IContentContext Content { get; }

        /// <summary>
        /// Provides access to the properties as stored in the entries in the space.
        /// </summary>
        IPropertiesContext Properties { get; }
    }

    public interface ISpaceConnection<out TTransport> : ISpaceConnection
        where TTransport: ISpaceTransport
    {
       new TTransport Transport { get; }
    }
}
