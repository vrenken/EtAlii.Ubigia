// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests;

internal sealed class InvalidTestRootHandlerMapper : IRootHandlerMapper
{
    public RootType Type { get; } = new("TestRoot");

    public IRootHandler[] AllowedRootHandlers { get; }

    public InvalidTestRootHandlerMapper()
    {
        var timePreparer = new TimePreparer();

        AllowedRootHandlers = new IRootHandler[]
        {
            new TimeRootByPathBasedYyyymmddhhmmssHandler(timePreparer),
            new TimeRootByPathBasedYyyymmddhhmmssHandler(timePreparer),
        };
    }
}
