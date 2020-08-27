namespace EtAlii.xTechnology.Networking
{
    using System;
    using System.Linq;
    using System.Net.NetworkInformation;

    public sealed class FreePortFinder
    {
        public int[] Find(PortRange range, int numberOfPorts)
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();

            var inUsePorts = Array.Empty<int>()
                .Concat(properties.GetActiveTcpConnections().Select(c => c.LocalEndPoint.Port)) // Ignore active connections            
                .Concat(properties.GetActiveTcpListeners().Select(l => l.Port)) // Ignore active tcp listeners
                .Concat(properties.GetActiveUdpListeners().Select(l => l.Port)) // Ignore active udp listeners
                .OrderBy(p => p)
                .Where(p => p >= range.LowerPort);

            return Enumerable
                .Range(range.LowerPort, range.UpperPort - range.LowerPort)
                .Except(inUsePorts)
                .Distinct()
                .GroupAdjacentBy((x, y) => x + 1 == y)
                .Select(g => g.ToArray())
                .First(g => g.Length >= numberOfPorts)
                .Take(numberOfPorts)
                .ToArray(); 
        }
    }
}
