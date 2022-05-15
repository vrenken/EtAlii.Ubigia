// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

using EtAlii.xTechnology.Hosting;

[assembly: Xunit.AssemblyTrait("Transport", "Grpc")]


// ReSharper disable once CheckNamespace
internal static partial class UnitTestSettings
{
    public static readonly PortRange NetworkPortRange = new(NetworkPortRangeStart + 13800, NetworkPortRangeStart + 13999);
}
