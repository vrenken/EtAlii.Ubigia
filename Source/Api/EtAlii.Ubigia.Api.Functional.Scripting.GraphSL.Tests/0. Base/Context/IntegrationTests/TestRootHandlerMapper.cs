﻿namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System;

    internal class TestRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get; }

        public IRootHandler[] AllowedRootHandlers { get; }

        public TestRootHandlerMapper()
        {
            Name = "TestRoot";
            AllowedRootHandlers = Array.Empty<IRootHandler>();
        }
    }
}
