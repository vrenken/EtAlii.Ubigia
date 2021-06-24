﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    internal class InvalidTestRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get; }

        public IRootHandler[] AllowedRootHandlers { get; }

        public InvalidTestRootHandlerMapper()
        {
            Name = "TestRoot";

            var timePreparer = new TimePreparer();

            AllowedRootHandlers = new IRootHandler[]
            {
                new TimeRootByPathBasedYyyymmddhhmmssHandler(timePreparer),
                new TimeRootByPathBasedYyyymmddhhmmssHandler(timePreparer),
            };
        }
    }
}
