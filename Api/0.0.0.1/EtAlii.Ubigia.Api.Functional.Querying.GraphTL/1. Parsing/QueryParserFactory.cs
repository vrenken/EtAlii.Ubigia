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
                new PathSubjectParsingScaffolding(),
                new ConstantHelpersScaffolding(), 
                new QueryParserScaffolding(),
                //new ConstantHelpersScaffolding(), 
                //new SubjectParsingScaffolding(), 
                //new PathSubjectParsingScaffolding(),
                //new OperatorParsingScaffolding(), 
            };
        }
    }
}
