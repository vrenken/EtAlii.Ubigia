// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

[assembly: Xunit.AssemblyTrait("Transport", "Grpc")]

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using EtAlii.xTechnology.Hosting;

    internal static class UnitTestSettings
    {
        public static readonly PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 13000, UnitTestConstants.NetworkPortRangeStart + 13199);
    }
}
