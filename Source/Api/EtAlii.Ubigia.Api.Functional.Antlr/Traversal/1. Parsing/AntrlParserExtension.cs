// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

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
