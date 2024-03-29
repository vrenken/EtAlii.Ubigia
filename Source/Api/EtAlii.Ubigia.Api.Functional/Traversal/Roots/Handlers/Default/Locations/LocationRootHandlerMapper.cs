// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

internal class LocationRootHandlerMapper : IRootHandlerMapper
{
    public RootType Type => RootType.Location;

    public IRootHandler[] AllowedRootHandlers { get; }

    public LocationRootHandlerMapper()
    {
        AllowedRootHandlers = new IRootHandler[]
        {
            new LocationRootByEmptyHandler(), // only root, no arguments, should be at the end.
        };
    }
}
