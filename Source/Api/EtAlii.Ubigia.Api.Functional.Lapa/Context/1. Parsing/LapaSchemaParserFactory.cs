// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaSchemaParserFactory : Factory<ISchemaParser, ParserOptions, ISchemaParserExtension>, ISchemaParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(ParserOptions options)
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
