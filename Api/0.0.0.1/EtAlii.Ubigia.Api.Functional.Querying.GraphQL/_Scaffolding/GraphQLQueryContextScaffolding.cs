namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.xTechnology.MicroContainer;
    using GraphQL;
    using GraphQL.Execution;
    using GraphQL.Http;
    using GraphQL.Validation;
    using GraphQL.Validation.Complexity;

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
            container.Register(() => new GraphSLScriptContextFactory().Create(_configuration));

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