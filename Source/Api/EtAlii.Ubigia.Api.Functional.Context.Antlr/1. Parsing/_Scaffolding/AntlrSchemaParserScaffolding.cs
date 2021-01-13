// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    internal class AntlrSchemaParserScaffolding : IScaffolding
    {
        private readonly IPathParserFactory _pathParserFactory;

        public AntlrSchemaParserScaffolding(IPathParserFactory pathParserFactory)
        {
            _pathParserFactory = pathParserFactory;
        }

        public void Register(Container container)
        {
            container.Register<IContextValidator, ContextValidator>();
            container.Register<ISchemaParser, AntlrSchemaParser>();
            container.Register(() => _pathParserFactory.Create(new ScriptParserConfiguration())); // TODO: This configuration should be done before.
        }
    }
}
