// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Threading.Tasks;

    public sealed partial class Ipv4FreePortFinder
    {
        private readonly Random _random = new(Environment.TickCount);
        private readonly TimeSpan _waitInterval = TimeSpan.FromSeconds(2);

        private ushort[] FindOnSystem(PortRange range, int numberOfPorts, IReadOnlyCollection<PortRegistration> registrations)
        {
            do
            {
                var properties = IPGlobalProperties.GetIPGlobalProperties();

                var inUsePorts = registrations.Select(r => r.Port)
                    .Concat(properties.GetActiveTcpConnections().SelectMany(c =>
                    {
                        return c.LocalEndPoint.Address.Equals(c.RemoteEndPoint.Address)
                            ? new[] {(ushort) c.LocalEndPoint.Port, (ushort) c.RemoteEndPoint.Port}
                            : new[] {(ushort) c.LocalEndPoint.Port};
                    })) // Ignore active connections
                    .Concat(properties.GetActiveTcpListeners().Select(l => (ushort)l.Port)) // Ignore active tcp listeners
                    .Concat(properties.GetActiveUdpListeners().Select(l => (ushort)l.Port)) // Ignore active udp listeners
                    .OrderBy(p => p)
                    .Where(p => p >= range.LowerPort && p <= range.UpperPort);

                var setToTakeFrom = Enumerable
                    .Range(range.LowerPort, (ushort) (range.UpperPort - range.LowerPort + 1))
                    .Select(port => (ushort) port)
                    .Except(inUsePorts)
                    .Distinct()
                    .GroupAdjacentBy((x, y) => x + 1 == y)
                    .Select(g => g.ToArray())
                    .Where(g => g.Length >= numberOfPorts)
                    .ToArray();

                var hasPorts = setToTakeFrom.Length != 0;
                if(hasPorts)
                {
                    var randomBatch = _random.Next(setToTakeFrom.Length);

                    return setToTakeFrom[randomBatch]
                        .Take(numberOfPorts)
                        .ToArray();
                }

                Task.Delay(_waitInterval).Wait();

            } while (true);
        }
    }
}
