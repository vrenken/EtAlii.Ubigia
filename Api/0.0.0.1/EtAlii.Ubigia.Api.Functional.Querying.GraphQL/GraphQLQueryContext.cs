namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Querying.GraphQL;
    using System.Threading.Tasks;
    using GraphQL;
    using GraphQL.Execution;

    internal class GraphQLQueryContext : IGraphQLQueryContext
    {
        private readonly IDataContext _dataContext;

        private readonly IDocumentExecuter _executor;
        private readonly IStaticSchema _staticSchema;
        private readonly IOperationProcessor _operationProcessor;
        private readonly IDocumentBuilder _builder;

        internal GraphQLQueryContext(IDataContext dataContext,
            IDocumentBuilder builder, 
            IDocumentExecuter executor,
            IStaticSchema staticSchema,
            IOperationProcessor operationProcessor)
        {
            _dataContext = dataContext;
            _builder = builder;
            _executor= executor;
            _staticSchema = staticSchema;
            _operationProcessor = operationProcessor;
        }
        
        public async Task<ExecutionResult> Execute(string query)//, Inputs inputs)
        {
            var inputs = new Inputs();
            var document = _builder.Build(query);
            var schema = await DynamicSchema.Create(_staticSchema, _operationProcessor, document);
            
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

            var result = await _executor.ExecuteAsync(configuration);

            return result;
        }
    }
}