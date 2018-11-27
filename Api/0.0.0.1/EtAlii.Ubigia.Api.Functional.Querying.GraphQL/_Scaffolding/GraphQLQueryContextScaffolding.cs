namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Querying.GraphQL;
    using EtAlii.xTechnology.MicroContainer;
    using GraphQL;
    using GraphQL.Execution;
    using GraphQL.Types;
    using GraphQL.Validation;
    using GraphQL.Validation.Complexity;

    internal class GraphQLQueryContextScaffolding : IScaffolding
    {
        private readonly IDataContext _dataContext;

        public GraphQLQueryContextScaffolding(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Register(Container container)
        {
            container.Register<IGraphQLQueryContext, GraphQLQueryContext>();

            container.Register<IDataContext>(() => _dataContext);
            container.Register<IScriptsSet>(() => _dataContext.Scripts);

            container.Register<IDependencyResolver>(() => new FuncDependencyResolver(container.GetInstance));

            container.Register<IDocumentValidator, DocumentValidator>();
            container.Register<IDocumentBuilder, GraphQLDocumentBuilder>();
            container.Register<IComplexityAnalyzer>(() => new ComplexityAnalyzer());

            container.Register<IDocumentExecuter>(() =>
            {
                var documentBuilder = container.GetInstance<IDocumentBuilder>();
                var documentValidator = container.GetInstance<IDocumentValidator>();
                var complexityAnalyzer = container.GetInstance<IComplexityAnalyzer>();
                return new DocumentExecuter(documentBuilder, documentValidator, complexityAnalyzer);
            });

            container.Register<IStaticQuery, StaticQuery>();
            container.Register<IStaticMutation, StaticMutation>();
            container.Register<IStaticSchema, StaticSchema>();
            
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