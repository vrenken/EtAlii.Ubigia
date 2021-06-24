// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ConnectivityDetailsExtension
    {
        public static ConnectivityDetails ToLocal(this WireProtocol.ConnectivityDetails connectivityDetails)
        {
            return new()
            {
                ManagementAddress = connectivityDetails.ManagementAddress,
                DataAddress = connectivityDetails.DataAddress
            };
        }

        public static IEnumerable<ConnectivityDetails> ToLocal(this IEnumerable<WireProtocol.ConnectivityDetails> connectivityDetailss)
        {
            return connectivityDetailss.Select(s => s.ToLocal());
        }
        public static WireProtocol.ConnectivityDetails ToWire(this ConnectivityDetails connectivityDetails)
        {
            return new()
            {
                ManagementAddress = connectivityDetails.ManagementAddress,
                DataAddress = connectivityDetails.ManagementAddress,
            };
        }

        public static IEnumerable<WireProtocol.ConnectivityDetails> ToWire(this IEnumerable<ConnectivityDetails> connectivityDetailss)
        {
            return connectivityDetailss.Select(s => s.ToWire());
        }
    }
}
