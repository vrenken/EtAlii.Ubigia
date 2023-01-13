// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using EtAlii.xTechnology.MicroContainer;

internal sealed class LapaScriptParserScaffolding : IScaffolding
{
    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<ITraversalValidator, TraversalValidator>();
        container.Register<IScriptParser, LapaScriptParser>();
    }
}
