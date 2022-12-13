// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class ProvidersRootHandlerMapper : IRootHandlerMapper
    {
        public RootType Type => RootType.Provider;

        public IRootHandler[] AllowedRootHandlers { get; }

        public ProvidersRootHandlerMapper()
        {
           AllowedRootHandlers = new IRootHandler[]
            {
                new ProvidersRootByEmptyHandler(), // only root, no arguments, should be at the end.
            };
        }
    }
}
