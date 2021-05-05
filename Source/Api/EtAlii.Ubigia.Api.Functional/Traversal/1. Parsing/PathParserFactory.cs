﻿// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class PathParserFactory : Factory<IPathParser, TraversalParserConfiguration, IScriptParserExtension>, IPathParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(TraversalParserConfiguration configuration)
        {
            return Array.Empty<IScaffolding>(); // Nothing to do here (for now).
        }
    }
}