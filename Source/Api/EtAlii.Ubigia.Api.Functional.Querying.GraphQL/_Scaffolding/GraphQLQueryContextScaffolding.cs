namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;
    using GraphQL;
    using GraphQL.Execution;
    using GraphQL.NewtonsoftJson;
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
            container.Register(() => new TraversalContextFactory().Create(_configuration));

            container.Register<IServiceProvider, GraphTypeServiceProvider>();

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
