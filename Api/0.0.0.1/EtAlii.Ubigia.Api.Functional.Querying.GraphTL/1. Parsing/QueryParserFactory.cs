// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class QueryParserFactory : Factory<IQueryParser, QueryParserConfiguration, IQueryParserExtension>, IQueryParserFactory
    {
        protected override IScaffolding[] CreateScaffoldings(QueryParserConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new QueryParserScaffolding(),
                new SequenceParsingScaffolding(), 
                new SubjectParsingScaffolding(), 
                new PathSubjectParsingScaffolding(),
                new OperatorParsingScaffolding(), 
                new ConstantHelpersScaffolding(), 
            };
        }
    }
}
