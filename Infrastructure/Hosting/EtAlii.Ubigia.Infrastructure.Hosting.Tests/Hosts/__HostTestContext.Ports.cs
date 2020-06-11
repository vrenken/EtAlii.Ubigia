namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    // using System;
    // using System.Collections.Generic;
    // using System.Linq;
    // using System.Net;
    // using System.Net.NetworkInformation;
    using EtAlii.xTechnology.Hosting;

    public abstract partial class HostTestContextBase<TTestHost>
        where TTestHost : class, IHost
    {
    //     protected IReadOnlyList<int> GetAvailableTcpPorts(int startingPort, int numberOfPorts)
    //     {
    //
    //         var properties = IPGlobalProperties.GetIPGlobalProperties();
    //         
    //         var inUsePorts = Array.Empty<int>()
    //             .Concat(properties.GetActiveTcpConnections().Select(c => c.LocalEndPoint.Port)) // Ignore active connections            
    //             .Concat(properties.GetActiveTcpListeners().Select(l => l.Port)) // Ignore active tcp listeners
    //             .Concat(properties.GetActiveUdpListeners().Select(l => l.Port)) // Ignore active udp listeners
    //             .OrderBy(p => p)
    //             .Where(p => p >= startingPort);
    //
    //         return EnumerableGroupAdjacentByExtension.GroupAdjacentBy(Enumerable
    // .Range(startingPort, IPEndPoint.MaxPort)
    // .Except(inUsePorts)
    // .Distinct(), (x, y) => x + 1 == y)
    //             .Select(g => g.ToArray())
    //             .First(g => g.Length >= numberOfPorts); 
    //     }
        //
        // protected IReadOnlyList<int> GetAvailableTcpPortsOld(int startingPort, int numberOfPorts)
        // {
        //     var result = new List<int>();
        //
        //     var portArray = new List<int>();
        //
        //     var properties = IPGlobalProperties.GetIPGlobalProperties();
        //
        //     // Ignore active connections
        //     var connections = properties.GetActiveTcpConnections();
        //     portArray.AddRange(from n in connections
        //         where n.LocalEndPoint.Port >= startingPort
        //         select n.LocalEndPoint.Port);
        //
        //     // Ignore active tcp listners
        //     var endPoints = properties.GetActiveTcpListeners();
        //     portArray.AddRange(from n in endPoints
        //         where n.Port >= startingPort
        //         select n.Port);
        //
        //     // Ignore active udp listeners
        //     endPoints = properties.GetActiveUdpListeners();
        //     portArray.AddRange(from n in endPoints
        //         where n.Port >= startingPort
        //         select n.Port);
        //
        //     portArray.Sort();
        //
        //     for (var i = startingPort; i < UInt16.MaxValue; i++)
        //         if (!portArray.Contains(i))
        //         {
        //             result.Add(i);
        //             if (--numberOfPorts == 0)
        //             {
        //                 break;
        //             }
        //         }
        //
        //     return result;
        // }
    }
}