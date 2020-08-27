namespace EtAlii.xTechnology.Networking
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.NetworkInformation;

    public sealed partial class Ipv4FreePortFinder
    {
        private ushort[] FindOnSystem(PortRange range, int numberOfPorts, IReadOnlyCollection<PortRegistration> registrations)
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();

            var inUsePorts = registrations.Select(r => r.Port)
                .Concat(properties.GetActiveTcpConnections().Select(c => (ushort)c.LocalEndPoint.Port)) // Ignore active connections            
                .Concat(properties.GetActiveTcpListeners().Select(l => (ushort)l.Port)) // Ignore active tcp listeners
                .Concat(properties.GetActiveUdpListeners().Select(l => (ushort)l.Port)) // Ignore active udp listeners
                .OrderBy(p => p)
                .Where(p => p >= range.LowerPort && p <= range.UpperPort);

            return Enumerable
                .Range(range.LowerPort, (ushort)(range.UpperPort - range.LowerPort + 1))
                .Select(port => (ushort)port)
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