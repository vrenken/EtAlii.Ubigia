// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    internal class SchemaParserFactory : Factory<ISchemaParser, SchemaParserConfiguration, ISchemaParserExtension>, ISchemaParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(SchemaParserConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new SchemaParserScaffolding(),
                new SequenceParsingScaffolding(),
                new SubjectParsingScaffolding(),
                new PathSubjectParsingScaffolding(),
                new OperatorParsingScaffolding(),
                new ConstantHelpersScaffolding(),
            };
        }
    }
}
