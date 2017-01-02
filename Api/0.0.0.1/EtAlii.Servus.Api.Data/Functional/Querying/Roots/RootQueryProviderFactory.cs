namespace EtAlii.Servus.Api.Data
{
    using EtAlii.xTechnology.MicroContainer;
    using Remotion.Linq;
    using Remotion.Linq.Parsing.ExpressionTreeVisitors.Transformation;
    using Remotion.Linq.Parsing.Structure;
    using System.Linq;
    using System.Linq.Expressions;
    using Remotion.Linq.Parsing.Structure.NodeTypeProviders;

    internal class RootQueryProviderFactory
    {
        private readonly Container _container;
        public RootQueryProviderFactory(Container container)
        {
            _container = container;
        }

        public RootQueryProvider Create()
        {
            var queryParser = CreateRootQueryParser();
            var queryExecutor = _container.GetInstance<RootQueryExecutor>();
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