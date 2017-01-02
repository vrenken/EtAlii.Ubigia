namespace EtAlii.Servus.Api.Data
{
    using System.Reflection;
    using EtAlii.xTechnology.MicroContainer;
    using Remotion.Linq;
    using Remotion.Linq.Parsing.ExpressionTreeVisitors.Transformation;
    using Remotion.Linq.Parsing.Structure;
    using System.Linq;
    using System.Linq.Expressions;
    using Remotion.Linq.Parsing.Structure.NodeTypeProviders;

    internal class NodeQueryProviderFactory
    {
        private readonly Container _container;

        public NodeQueryProviderFactory(Container container)
        {
            _container = container;
        }

        public NodeQueryProvider Create()
        {
            var queryParser = CreateNodeQueryParser();
            var queryExecutor = _container.GetInstance<NodeQueryExecutor>();
            var queryProvider = new NodeQueryProvider(queryParser, queryExecutor);
            return queryProvider;
        }

        private IQueryParser CreateNodeQueryParser()
        {
            var nodeTypeRegistry = new MethodInfoBasedNodeTypeRegistry();
            // Register custom node parsers here:
            //nodeTypeRegistry.Register(new MethodInfo[] { NodeSetMethod.SelectByPath }, typeof(SelectByPathSourceExpressionNode));
            //nodeTypeRegistry.Register(new MethodInfo[] { NodeSetMethod.SelectByRoot }, typeof(SelectByRootOperator));
            //nodeTypeRegistry.Register(new MethodInfo[] { NodeSetMethod.SelectByIdentifier }, typeof(SelectByIdentifierOperator));
            nodeTypeRegistry.Register(new MethodInfo[] { NodeExtensionMethod.Add }, typeof(AddResultOperatorExpressionNode));

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