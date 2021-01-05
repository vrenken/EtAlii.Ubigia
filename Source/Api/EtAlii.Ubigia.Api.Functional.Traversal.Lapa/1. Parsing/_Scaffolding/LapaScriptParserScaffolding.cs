// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaScriptParserScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IScriptValidator, ScriptValidator>();
            container.Register<IScriptParser, LapaScriptParser>();
        }
    }
}
