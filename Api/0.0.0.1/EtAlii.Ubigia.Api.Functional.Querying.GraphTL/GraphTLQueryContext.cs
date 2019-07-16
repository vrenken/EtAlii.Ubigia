namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class GraphTLQueryContext : IGraphTLQueryContext
    {
        private readonly IQueryProcessorFactory _queryProcessorFactory;
        private readonly IQueryParserFactory _queryParserFactory;
        private readonly IGraphSLScriptContext _scriptContext;

        protected internal GraphTLQueryContext(
            IQueryProcessorFactory queryProcessorFactory, 
            IQueryParserFactory queryParserFactory, 
            IGraphSLScriptContext scriptContext)
        {
            _queryProcessorFactory = queryProcessorFactory;
            _queryParserFactory = queryParserFactory;
            _scriptContext = scriptContext;
        }

        public QueryParseResult Parse(string text)
        {
            var queryParserConfiguration = new QueryParserConfiguration();
                //.Use(_logicalContext.Configuration)
                //.Use(_diagnostics)
            var parser = _queryParserFactory.Create(queryParserConfiguration);
            return parser.Parse(text);
        }

        public Task<QueryProcessingResult> Process(Query query, IQueryScope scope)
        {
            var configuration = new QueryProcessorConfiguration()
                .Use(scope)
                .Use(_scriptContext);
            var processor = _queryProcessorFactory.Create(configuration);
            return processor.Process(query);
        }

        public Task<QueryProcessingResult> Process(string[] text, IQueryScope scope)
        {
            var queryParseResult = Parse(string.Join(Environment.NewLine, text));

            if (queryParseResult.Errors.Any())
            {
                var firstError = queryParseResult.Errors.First();
                throw new QueryParserException(firstError.Message, firstError.Exception);
            }

            return Process(queryParseResult.Query, scope);
        }


        public Task<QueryProcessingResult> Process(string text, params object[] args)
        {
            text = string.Format(text, args);
            return Process(text);
        }

        public Task<QueryProcessingResult> Process(string[] text)
        {
            return Process(string.Join(Environment.NewLine, text));
        }

        public Task<QueryProcessingResult> Process(string text)
        {
            var queryParseResult = Parse(text);

            if (queryParseResult.Errors.Any())
            {
                var firstError = queryParseResult.Errors.First();
                throw new QueryParserException(firstError.Message, firstError.Exception);
            }

            var scope = new QueryScope();

            return Process(queryParseResult.Query, scope);
        }
    }
}