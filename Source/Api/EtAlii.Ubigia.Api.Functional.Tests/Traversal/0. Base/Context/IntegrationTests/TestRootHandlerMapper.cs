﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests;

using System;

internal sealed class TestRootHandlerMapper : IRootHandlerMapper
{
    public RootType Type { get; } = new("TestRoot");

    public IRootHandler[] AllowedRootHandlers { get; }

    public TestRootHandlerMapper()
    {
        AllowedRootHandlers = Array.Empty<IRootHandler>();
    }
}
