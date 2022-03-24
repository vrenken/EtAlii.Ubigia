// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;

    internal sealed class TestRoot2HandlerMapper : IRootHandlerMapper
    {
        public string Name { get; }

        public IRootHandler[] AllowedRootHandlers { get; }

        public TestRoot2HandlerMapper()
        {
            Name = "TestRoot2";
            AllowedRootHandlers = Array.Empty<IRootHandler>();
        }
    }
}
