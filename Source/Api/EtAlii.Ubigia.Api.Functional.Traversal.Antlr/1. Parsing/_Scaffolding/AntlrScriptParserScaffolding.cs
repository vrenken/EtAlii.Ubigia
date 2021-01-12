// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class AntlrScriptParserScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ITraversalValidator, TraversalValidator>();
            container.Register<IScriptParser, AntlrScriptParser>();
        }
    }
}
