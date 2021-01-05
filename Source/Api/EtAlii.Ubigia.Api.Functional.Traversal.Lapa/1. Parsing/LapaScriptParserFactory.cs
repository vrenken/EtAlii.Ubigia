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
                new LapaScriptParserScaffolding(),
                new SequenceParsingScaffolding(),
                new SubjectParsingScaffolding(),
                new PathSubjectParsingScaffolding(),
                new OperatorParsingScaffolding(),
                new ConstantHelpersScaffolding(),
            };
        }
    }
}
