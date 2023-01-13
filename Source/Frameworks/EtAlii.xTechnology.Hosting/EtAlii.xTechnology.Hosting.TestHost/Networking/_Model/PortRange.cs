// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting;

using System;
using System.Collections;
using System.Collections.Generic;

public readonly struct PortRange : IEnumerable<ushort>
{
    public ushort LowerPort { get; }
    public ushort UpperPort { get; }

    public PortRange(ushort lowerPortInclusive, ushort upperPortInclusive)
    {
        if (lowerPortInclusive > upperPortInclusive)
        {
            throw new InvalidOperationException("Unable to create a port range - upper port below the lower port");
        }
        LowerPort = lowerPortInclusive;
        UpperPort = upperPortInclusive;
    }

    public bool IsInRange(ushort port) => port >= LowerPort && port <= UpperPort;

    public ushort this[ushort index] => (ushort)(LowerPort + index);
    public ushort Count => (ushort)(UpperPort - LowerPort);

    public IEnumerator<ushort> GetEnumerator()
    {
        for (var port = LowerPort; port <= UpperPort; port++)
        {
            yield return port;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString() => $"{LowerPort}-{UpperPort}";
}
