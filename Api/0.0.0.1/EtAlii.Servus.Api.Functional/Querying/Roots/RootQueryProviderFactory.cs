namespace EtAlii.Servus.Api.Functional
{
    using Remotion.Linq.Parsing.ExpressionVisitors.Transformation;
    using Remotion.Linq.Parsing.Structure;
    using Remotion.Linq.Parsing.Structure.NodeTypeProviders;

    internal class RootQueryProviderFactory : IRootQueryProviderFactory
    {
        private readonly IRootQueryExecutorFactory _rootQueryExecutorFactory;

        public RootQueryProviderFactory(IRootQueryExecutorFactory rootQueryExecutorFactory)
        {
            _rootQueryExecutorFactory = rootQueryExecutorFactory;
        }

        public RootQueryProvider Create()
        {
            var queryParser = CreateRootQueryParser();
            var queryExecutor = _rootQueryExecutorFactory.Create();
            return new RootQueryProvider(queryParser, queryExecutor);
        }
        
        private IQueryParser CreateRootQueryParser()
        {
            var nodeTypeRegistry = new MethodInfoBasedNodeTypeRegistry();
            // Register custom node parsers here:
            // nodeTypeRegistry.Register (MyExpressionNode.SupportedMethods, typeof (MyExpressionNode));
            // Alternatively, use the CreateFromTypes factory method.
            // Use MethodNameBasedNodeTypeRegistry to register parsers by query operator name instead of MethodInfo.

            var nodeTypeProvider = ExpressionTreeParser.CreateDefaultNodeTypeProvider();
            nodeTypeProvider.InnerProviders.Add(nodeTypeRegistry);

            var transformerRegistry = ExpressionTransformerRegistry.CreateDefault();
            // Register custom expression transformers executed _after_ partial evaluation here (this should be the default):
            // transformerRegistry.Register (new MyExpressionTransformer());

            var processor = ExpressionTreeParser.CreateDefaultProcessor(transformerRegistry);

            // To register custom expression transformers executed _before_ partial evaluation, use this code:
            // var earlyTransformerRegistry = new ExpressionTransformerRegistry();
            // earlyTransformerRegistry.Register (new MyEarlyExpressionTransformer());
            // processor.InnerProcessors.Insert (0, new TransformingExpressionTreeProcessor (tranformationProvider));

            // Add custom processors here (use Insert (0, ...) to add at the beginning):
            // processor.InnerProcessors.Add (new MyExpressionTreeProcessor());

            var expressionTreeParser = new ExpressionTreeParser(nodeTypeProvider, processor);
            var queryParser = new QueryParser(expressionTreeParser);

            return queryParser;
        }
    }
}