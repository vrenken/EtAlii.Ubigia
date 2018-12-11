namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Querying.GraphQL;
    using System.Threading.Tasks;
    using GraphQL;
    using GraphQL.Execution;
    using GraphQL.Http;

    internal class GraphQLQueryContext : IGraphQLQueryContext
    {
        private readonly IDocumentExecuter _executor;
        private readonly IStaticSchema _staticSchema;
        private readonly IOperationProcessor _operationProcessor;
        private readonly IFieldProcessor _fieldProcessor;
        private readonly IDocumentBuilder _builder;
        private readonly IDocumentWriter _documentWriter;

        internal GraphQLQueryContext(
            IDocumentBuilder builder,
            IDocumentWriter documentWriter,
            IDocumentExecuter executor,
            IStaticSchema staticSchema,
            IOperationProcessor operationProcessor, 
            IFieldProcessor fieldProcessor)
        {
            _builder = builder;
            _documentWriter = documentWriter;
            _executor= executor;
            _staticSchema = staticSchema;
            _operationProcessor = operationProcessor;
            _fieldProcessor = fieldProcessor;
        }
        
        public async Task<QueryExecutionResult> Execute(string query)//, Inputs inputs)
        {
            var inputs = new Inputs();
            var document = _builder.Build(query);
            var schema = await DynamicSchema.Create(_staticSchema, _operationProcessor, _fieldProcessor, document);
            
            // The current thinking is to make these dependent of some of the Ubigia directives provided by the query.
            var configuration = new ExecutionOptions
            {
                // First we need to know the document to know where to start path traversal.
                // This should not have any consequences for the further execution.
                Document = document,

                // We do this by always returning a dynamic schema which includes everything from the static schema.
                //_.Schema = DynamicSchema.Create(schema, request.Query);
                Schema = schema,
                Query = query,
                OperationName = null, //operationName;//request.OperationName;
                Inputs = inputs, //request.Variables.ToInputs();
                UserContext = new UserContext {User = null} // ctx.User ;
            };

            var executionResult = await _executor.ExecuteAsync(configuration);

            var dataAsString = await _documentWriter.WriteToStringAsync(executionResult);
                
            return new QueryExecutionResult(executionResult, dataAsString);
        }
    }
}