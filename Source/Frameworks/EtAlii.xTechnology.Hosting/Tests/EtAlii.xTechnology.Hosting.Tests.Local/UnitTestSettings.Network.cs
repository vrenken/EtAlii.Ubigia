// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

using EtAlii.xTechnology.Hosting;

// ReSharper disable once CheckNamespace
internal static partial class UnitTestSettings
{
    // For our hosting tests we need a bigger range.
    public static readonly PortRange NetworkPortRange = new(NetworkPortRangeStart + 17000, NetworkPortRangeStart + 17499);
}
