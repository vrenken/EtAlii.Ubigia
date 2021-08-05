// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    /// <summary>
    /// The basic interface for a data oriented connection.
    /// (i.e. not management oriented)
    /// </summary>
    public interface IDataConnection
    {
        /// <summary>
        /// The storage which the connection talks to.
        /// </summary>
        Storage Storage { get; }

        /// <summary>
        /// The account used to connect to the storage.
        /// </summary>
        Account Account { get; }

        /// <summary>
        /// The space on the storage which the connection talks to.
        /// </summary>
        Space Space { get; }

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

        /// <summary>
        /// Returns true when a connection has been established.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// The Options used to instantiate this DataConnection.
        /// </summary>
        IDataConnectionOptions Options { get; }

        /// <summary>
        /// Connect to the specified storage/space using the given credentials.
        /// </summary>
        /// <returns></returns>
        Task Open();

        /// <summary>
        /// Disconnect from the specified storage/space.
        /// </summary>
        /// <returns></returns>
        Task Close();
    }
}
