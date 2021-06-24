// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    internal class AntrlParserExtension : IScriptParserExtension, IScriptProcessorExtension
    {
        public void Initialize(Container container)
        {
            container.Register<IPathParser, AntlrPathParser>();

            container.Register<ITraversalValidator, TraversalValidator>();
            container.Register<IScriptParser, AntlrScriptParser>();
        }
    }
}
