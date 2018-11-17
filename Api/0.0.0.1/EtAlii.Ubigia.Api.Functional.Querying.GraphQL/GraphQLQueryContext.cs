namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Querying.GraphQL;
    using System.Threading.Tasks;
    using GraphQL;
    using GraphQL.Execution;

    internal class GraphQLQueryContext : IGraphQLQueryContext
    {
        private readonly IDataContext _dataContext;

        private readonly IScriptsSet _scriptsSet;
        private readonly IDocumentExecuter _executor;
        private readonly IStaticSchema _staticSchema;
        private readonly IDocumentBuilder _builder;

        internal GraphQLQueryContext(IDataContext dataContext,
            IScriptsSet scriptsSet, 
            IDocumentBuilder builder, 
            IDocumentExecuter executor,
            IStaticSchema staticSchema)
        {
            _dataContext = dataContext;
            _scriptsSet = scriptsSet;
            _builder = builder;
            _executor= executor;
            _staticSchema = staticSchema;
        }
        
        public async Task<ExecutionResult> Execute(string query, Inputs inputs)
        {
            var result = await _executor.ExecuteAsync(configuration =>
            {
                var task = Task.Run(async () =>
                {
                    // The current thinking is to make these dependent of some of the Ubigia directives provided by the query.

                    // First we need to know the document to know where to start path traversal.
                    // This should not have any consequences for the further execution.
                    configuration.Document = _builder.Build(query);

                    // We do this by always returning a dynamic schema which includes everything from the static schema.
                    //_.Schema = DynamicSchema.Create(schema, request.Query);
                    configuration.Schema = await DynamicSchema.Create(_staticSchema, _scriptsSet, configuration.Document);
                    configuration.Query = query;
                    configuration.OperationName = null;//operationName;//request.OperationName;
                    configuration.Inputs = inputs;//request.Variables.ToInputs();
                    configuration.UserContext = new UserContext
                    {
                        User = null // ctx.User
                    };
                });
                task.Wait();
            });

            return result;
        }
    }
}