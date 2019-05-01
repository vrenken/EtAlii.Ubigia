// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptParserFactory : Factory<IScriptParser, ScriptParserConfiguration, IScriptParserExtension>, IScriptParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(ScriptParserConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new SequenceParsingScaffolding(), 
                new ScriptParserScaffolding(),
                new ConstantHelpersScaffolding(), 
                new SubjectParsingScaffolding(), 
                new PathSubjectParsingScaffolding(),
                new OperatorParsingScaffolding(), 
            };
        }
    }
}
