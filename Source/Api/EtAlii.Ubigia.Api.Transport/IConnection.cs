// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    /// <summary>
    /// The basic interface for all network connections.
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// The storage to which the connection talks.
        /// </summary>
        Storage Storage { get; }

        // TODO: is a must.
        //Account Account [ get ]

        /// <summary>
        /// Returns true when a connection with the server has been made.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Disconnect from the remote service.
        /// </summary>
        /// <returns></returns>
        Task Close();

        /// <summary>
        /// Connect using the given credentials.
        /// </summary>
        /// <returns></returns>
        Task Open(string accountName, string password);
    }
}
