// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptParserScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IScriptParser, ScriptParser>();

            // Path helpers
            container.Register<IPathRelationParserBuilder, PathRelationParserBuilder>();
        }
    }
}
