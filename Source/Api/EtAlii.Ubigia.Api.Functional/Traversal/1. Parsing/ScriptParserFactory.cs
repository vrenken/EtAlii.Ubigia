// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class ScriptParserFactory : Factory<IScriptParser, IFunctionalOptions, IFunctionalExtension>, IScriptParserFactory
    {
        // TODO: Should we remove the ScriptParserFactory and use an injected IScriptParser singleton instance instead?
        protected override IScaffolding[] CreateScaffoldings(IFunctionalOptions options)
        {
            return Array.Empty<IScaffolding>();
        }
    }
}
