// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaSchemaParserFactory : Factory<ISchemaParser, SchemaParserConfiguration, ISchemaParserExtension>, ISchemaParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(SchemaParserConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new LapaSchemaParserScaffolding(),

                new LapaSequenceParsingScaffolding(),
                new LapaSubjectParsingScaffolding(),
                new LapaOperatorParsingScaffolding(),
                new LapaPathSubjectParsingScaffolding(),
                new LapaConstantParsingScaffolding(),
            };
        }
    }
}
