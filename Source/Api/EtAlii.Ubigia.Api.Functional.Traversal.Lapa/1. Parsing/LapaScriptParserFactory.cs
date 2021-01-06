// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaScriptParserFactory : Factory<IScriptParser, ScriptParserConfiguration, IScriptParserExtension>, IScriptParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(ScriptParserConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new LapaSequenceParsingScaffolding(),
                new LapaSubjectParsingScaffolding(),
                new LapaOperatorParsingScaffolding(),
                new LapaScriptParserScaffolding(),
                new LapaPathSubjectParsingScaffolding(),
                new LapaConstantParsingScaffolding(),
            };
        }
    }
}
