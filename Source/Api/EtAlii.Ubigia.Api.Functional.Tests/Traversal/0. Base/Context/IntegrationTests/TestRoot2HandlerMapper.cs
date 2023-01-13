// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests;

using System;

internal sealed class TestRoot2HandlerMapper : IRootHandlerMapper
{
    public RootType Type { get; } = new("TestRoot2");

    public IRootHandler[] AllowedRootHandlers { get; }

    public TestRoot2HandlerMapper()
    {
        AllowedRootHandlers = Array.Empty<IRootHandler>();
    }
}
