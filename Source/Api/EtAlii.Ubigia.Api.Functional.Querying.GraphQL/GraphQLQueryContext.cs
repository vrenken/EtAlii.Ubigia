namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Threading.Tasks;
    using GraphQL;
    using GraphQL.Execution;
    using GraphQL.Http;

    internal class GraphQLQueryContext : IGraphQLQueryContext
    {
        private readonly IDocumentExecuter _executor;
        private readonly IDependencyResolver _dependencyResolver;
        private readonly IOperationProcessor _operationProcessor;
        private readonly IFieldProcessor _fieldProcessor;
        private readonly IDocumentBuilder _builder;
        private readonly IDocumentWriter _documentWriter;

        public GraphQLQueryContext(
            IDocumentBuilder builder,
            IDocumentWriter documentWriter,
            IDocumentExecuter executor,
            IDependencyResolver dependencyResolver,
            IOperationProcessor operationProcessor, 
            IFieldProcessor fieldProcessor)
        {
            _builder = builder;
            _documentWriter = documentWriter;
            _executor = executor;
            _dependencyResolver = dependencyResolver;
            _operationProcessor = operationProcessor;
            _fieldProcessor = fieldProcessor;
        }

        public Task<QueryParseResult> Parse(string text)
        {
            global::GraphQL.Language.AST.Document document = null;
            var errors = Array.Empty<QueryParserError>();
            try
            {
                document = _builder.Build(text);
            }
            catch (Exception e)
            {
                errors = new[] { new QueryParserError(e, e.Message, 0, 0) };
            }
            
            var query = new Query(document, text);
            var result = new QueryParseResult(text, query, errors);
            return Task.FromResult(result);
        }
        
        public async Task<GraphQLQueryProcessingResult> Process(Query query) 
        {
            var document = query.Document;
            var schema = await DynamicSchema.Create(_dependencyResolver, _operationProcessor, _fieldProcessor, document);
            var inputs = new Inputs();

            // The current thinking is to make these dependent of some of the Ubigia directives provided by the query.
            var configuration = new ExecutionOptions
            {
                // First we need to know the document to know where to start path traversal.
                // This should not have any consequences for the further execution.
                Document = document,

                // We do this by always returning a dynamic schema which includes everything from the static schema.
                //_.Schema = DynamicSchema.Create(schema, request.Query)
                Schema = schema,
                Query = query.Text,
                OperationName = null, //operationName//request.OperationName
                Inputs = inputs, //request.Variables.ToInputs()
                UserContext = new UserContext {User = null} // ctx.User 
            };

            var executionResult = await _executor.ExecuteAsync(configuration);

            var dataAsString = await _documentWriter.WriteToStringAsync(executionResult);
                
            return new GraphQLQueryProcessingResult(executionResult, dataAsString);
        }
    }
}