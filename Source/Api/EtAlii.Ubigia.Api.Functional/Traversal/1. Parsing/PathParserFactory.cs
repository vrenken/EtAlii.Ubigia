// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class PathParserFactory : Factory<IPathParser, TraversalParserOptions, IScriptParserExtension>, IPathParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(TraversalParserOptions options)
        {
            return Array.Empty<IScaffolding>(); // Nothing to do here (for now).
        }
    }
}
