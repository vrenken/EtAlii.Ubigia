// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class ProvidersRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get; }

        public IRootHandler[] AllowedRootHandlers { get; }

        public ProvidersRootHandlerMapper()
        {
            Name = "providers";

           AllowedRootHandlers = new IRootHandler[]
            {
                new ProvidersRootByEmptyHandler(), // only root, no arguments, should be at the end.
            };
        }
    }
}
