// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.xTechnology.MicroContainer;

    public class AntlrSchemaParserFactory : Factory<ISchemaParser, SchemaParserConfiguration, ISchemaParserExtension>, ISchemaParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(SchemaParserConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new AntlrSchemaParserScaffolding(configuration.TraversalParserConfiguration),
            };
        }
    }
}
