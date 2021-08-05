// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using EtAlii.Ubigia.Api.Transport;

    public interface IFabricContext : IDisposable
    {
        /// <summary>
        /// The Options used to instantiate this Context.
        /// </summary>
        IFabricContextOptions Options { get; }

        /// <summary>
        /// The Connection property provides access to the connection used to communicate with the backend.
        /// </summary>
        IDataConnection Connection { get; }

        /// <summary>
        /// The Roots property provides immutable CRUD capabilities to the roots of the graph.
        /// Use it to start a traversal.
        /// </summary>
        IRootContext Roots { get; }

        /// <summary>
        /// The Entries property provides immutable CRUD capabilities to the entries of the graph.
        /// </summary>
        IEntryContext Entries { get; }

        /// <summary>
        /// The Content property provides immutable CRUD capabilities to the content stored in the entries of the graph.
        /// </summary>
        IContentContext Content { get; }
        /// <summary>
        /// The Properties property provides immutable CRUD capabilities to the properties stored in the entries of the graph.
        /// </summary>
        IPropertiesContext Properties { get; }
    }
}
