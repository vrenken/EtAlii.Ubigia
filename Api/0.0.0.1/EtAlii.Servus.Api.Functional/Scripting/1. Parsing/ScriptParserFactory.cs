// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptParserFactory : IScriptParserFactory
    {
        public IScriptParser Create(IScriptParserConfiguration configuration)
        {
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new SequenceParsingScaffolding(), 
                new ScriptParserScaffolding(),
                new ConstantHelpersScaffolding(), 
                new SubjectParsingScaffolding(), 
                new OperatorParsingScaffolding(), 
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            return container.GetInstance<IScriptParser>();
        }
    }
}
