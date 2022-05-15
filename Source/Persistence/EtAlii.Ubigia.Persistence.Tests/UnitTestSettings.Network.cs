﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

using EtAlii.xTechnology.Hosting;

internal static partial class UnitTestSettings
{
    public static readonly PortRange NetworkPortRange = new(NetworkPortRangeStart + 17500, NetworkPortRangeStart + 17999);
}
