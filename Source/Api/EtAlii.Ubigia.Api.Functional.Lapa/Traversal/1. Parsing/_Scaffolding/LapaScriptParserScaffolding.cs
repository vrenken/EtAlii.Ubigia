// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaScriptParserScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ITraversalValidator, TraversalValidator>();
            container.Register<IScriptParser, LapaScriptParser>();
        }
    }
}
