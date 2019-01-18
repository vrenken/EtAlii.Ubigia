namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;

    internal class OperationProcessor : IOperationProcessor
    {
        private readonly INodesDirectiveHandler _nodesDirectiveHandler;

        public OperationProcessor(
            INodesDirectiveHandler nodesDirectiveHandler)
        {
            _nodesDirectiveHandler = nodesDirectiveHandler;
        }

        public async Task<OperationContext> Process(
            Operation operation, 
            ComplexGraphType<object> query, 
            Dictionary<System.Type, GraphType> graphTypes)
        {
            var nodesDirectiveResults = new List<NodesDirectiveResult>();
            var nodesDirectives = operation.Directives
                .Where(directive => directive.Name == "nodes")
                .ToArray();
            foreach (var nodesDirective in nodesDirectives)
            {
                var directiveResult = await _nodesDirectiveHandler.Handle(nodesDirective);
                nodesDirectiveResults.Add(directiveResult);
            }

            var results = nodesDirectiveResults.ToArray();
            var registration = OperationContext.FromDirectives(results);
            registration.GraphType = query;
            
            return registration;
        }
    }
}