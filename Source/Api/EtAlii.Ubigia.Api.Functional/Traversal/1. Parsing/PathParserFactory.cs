// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class PathParserFactory : Factory<IPathParser, ParserOptions, IScriptParserExtension>, IPathParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(ParserOptions options)
        {
            return Array.Empty<IScaffolding>(); // Nothing to do here (for now).
        }
    }
}
