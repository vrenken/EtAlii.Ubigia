// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    public class ScriptParserFactory : Factory<IScriptParser, IFunctionalOptions, IScriptParserExtension>, IScriptParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(IFunctionalOptions options)
        {
            return new IScaffolding[]
            {
                new ScriptParserScaffolding(options)
            };
        }
    }
}
