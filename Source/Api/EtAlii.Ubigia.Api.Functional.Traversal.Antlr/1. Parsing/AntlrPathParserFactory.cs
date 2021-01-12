// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class AntlrPathParserFactory : Factory<IPathParser, ScriptParserConfiguration, IScriptParserExtension>, IPathParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(ScriptParserConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new AntlrScriptParserScaffolding(),
             };
        }
    }
}
