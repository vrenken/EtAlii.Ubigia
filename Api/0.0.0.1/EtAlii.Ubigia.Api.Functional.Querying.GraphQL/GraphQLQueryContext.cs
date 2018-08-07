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
//        private readonly GraphQLSettings _settings;
        private readonly IDocumentExecuter _executer;
        private readonly IStaticSchema _staticSchema;
//        private readonly IDocumentWriter _writer;
        private readonly IDocumentBuilder _builder;

        internal GraphQLQueryContext(IDataContext dataContext,
            IScriptsSet scriptsSet, 
            IDocumentBuilder builder, 
//            IDocumentWriter writer, 
            IDocumentExecuter executer,
            IStaticSchema staticSchema)
        {
            _dataContext = dataContext;
            _scriptsSet = scriptsSet;
            _builder = builder;
//            _writer = writer;
            _executer = executer;
            _staticSchema = staticSchema;
        }
        
        
//        public async Task Invoke(HttpContext context, ISchema schema)
//        {
//            if (!IsGraphQLRequest(context))
//            {
//                await _next(context);
//                return;
//            }
//
//            await ExecuteAsync(context, schema);
//        }

//        private bool IsGraphQLRequest(HttpContext context)
//        {
//            return context.Request.Path.StartsWithSegments(_settings.Path)
//                && string.Equals(context.Request.Method, "POST", StringComparison.OrdinalIgnoreCase);
//        }

        public async Task<ExecutionResult> Execute(string operationName, string query, Inputs inputs)
        {
            //var request = Deserialize<GraphQLRequest>(query);

            var result = await _executer.ExecuteAsync(_ =>
            {
                // The current thinking is to make these dependent of some of the Ubigia directives provided by the query.

                // First we need to know the document to know where to start path traversal.
                // This should not have any consequences for the further execution.
                _.Document = _builder.Build(query);

                // We do this by always returning a dynamic schema which includes everything from the static schema.
//                _.Schema = DynamicSchema.Create(schema, request.Query);
                _.Schema = DynamicSchema.Create(_staticSchema, _scriptsSet, _.Document);
                _.Query = query;
                _.OperationName = null;//operationName;//request.OperationName;
                _.Inputs = inputs;//request.Variables.ToInputs();
                _.UserContext = new GraphQLUserContext
                {
                    User = null // ctx.User
                }; //_settings.BuildUserContext?.Invoke(context);
            });

            return result;
            //await WriteResponseAsync(context, result);
        }

//        private async Task WriteResponseAsync(HttpContext context, ExecutionResult result)
//        {
//            var json = _writer.Write(result);
//
//            context.Response.ContentType = "application/json";
//            context.Response.StatusCode = result.Errors?.Any() == true ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK;
//
//            await context.Response.WriteAsync(json);
//        }

//        public static T Deserialize<T>(Stream s)
//        {
//            using (var reader = new StreamReader(s))
//            using (var jsonReader = new JsonTextReader(reader))
//            {
//                var ser = new JsonSerializer();
//                return ser.Deserialize<T>(jsonReader);
//            }
//        }
    }
}