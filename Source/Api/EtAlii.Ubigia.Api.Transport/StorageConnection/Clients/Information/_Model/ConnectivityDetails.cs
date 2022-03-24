// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    /// <summary>
    /// A model class that contains connectivity related details.
    /// </summary>
    public sealed class ConnectivityDetails
    {
        /// <summary>
        /// The transport specific management API address to which the client is connected.
        /// </summary>
        public string ManagementAddress { get; set; }

        /// <summary>
        /// The transport specific data API address to which the client is connected.
        /// </summary>
        public string DataAddress { get; set; }
    }
}
