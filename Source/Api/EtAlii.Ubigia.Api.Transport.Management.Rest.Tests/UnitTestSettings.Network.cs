// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

[assembly: Xunit.AssemblyTrait("Transport", "Rest")]

namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using EtAlii.xTechnology.Hosting;

    internal static class UnitTestSettings
    {
        public static readonly PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 14200, UnitTestConstants.NetworkPortRangeStart + 14399);
    }
}
