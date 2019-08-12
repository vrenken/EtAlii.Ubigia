namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using EtAlii.xTechnology.MicroContainer;
    using global::GraphQL;
    using global::GraphQL.Execution;
    using global::GraphQL.Http;
    using global::GraphQL.Validation;
    using global::GraphQL.Validation.Complexity;

    internal class GraphQLQueryContextScaffolding : IScaffolding
    {
        private readonly GraphQLQueryContextConfiguration _configuration;

        public GraphQLQueryContextScaffolding(GraphQLQueryContextConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Register(Container container)
        {
            container.Register<IGraphQLQueryContext, GraphQLQueryContext>();
            container.Register<IGraphSLScriptContext>(() => new GraphSLScriptContextFactory().Create(_configuration));

            container.Register<IDependencyResolver>(() => new FuncDependencyResolver(container.GetInstance));

            container.Register<IDocumentValidator, DocumentValidator>();
            container.Register<IDocumentBuilder, GraphQLDocumentBuilder>();
            container.Register<IComplexityAnalyzer>(() => new ComplexityAnalyzer());

            container.Register<IDocumentWriter>(() => new DocumentWriter(indent: true));
            
            container.Register<IDocumentExecuter>(() =>
            {
                var documentBuilder = container.GetInstance<IDocumentBuilder>();
                var documentValidator = container.GetInstance<IDocumentValidator>();
                var complexityAnalyzer = container.GetInstance<IComplexityAnalyzer>();
                return new DocumentExecuter(documentBuilder, documentValidator, complexityAnalyzer);
            });
            
            container.Register<IOperationProcessor, OperationProcessor>();
            container.Register<IFieldProcessor, FieldProcessor>();
            
            container.Register<INodesDirectiveHandler, NodesDirectiveHandler>();
            container.Register<INodesFieldAdder, NodesFieldAdder>();
            container.Register<IIdDirectiveHandler, IdDirectiveHandler>();
            container.Register<IIdFieldAdder, IdFieldAdder>();
            
            container.Register<INodeFetcher, NodeFetcher>();
            container.Register<IComplexFieldTypeBuilder, ComplexFieldTypeBuilder>();
            container.Register<IScalarFieldTypeBuilder, ScalarFieldTypeBuilder>();
            container.Register<IListFieldTypeBuilder, ListFieldTypeBuilder>();

        }
    }
}