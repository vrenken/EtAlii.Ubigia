// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

[assembly: Xunit.AssemblyTrait("Transport", "Rest")]

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using EtAlii.xTechnology.Hosting;

    internal static class UnitTestSettings
    {
        public static readonly PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 19000, UnitTestConstants.NetworkPortRangeStart + 19499);
    }
}
