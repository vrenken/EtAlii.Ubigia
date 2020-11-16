namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GraphQL.Language.AST;
    using GraphQL.Types;

    internal class OperationProcessor : IOperationProcessor
    {
        private readonly INodesDirectiveHandler _nodesDirectiveHandler;

        public OperationProcessor(INodesDirectiveHandler nodesDirectiveHandler)
        {
            _nodesDirectiveHandler = nodesDirectiveHandler;
        }

        public async Task<OperationContext> Process(
            Operation operation, 
            ComplexGraphType<object> query)
        {
            var nodesDirectiveResults = new List<NodesDirectiveResult>();
            var nodesDirectives = operation.Directives != null
                ? operation.Directives.Where(directive => directive.Name == "nodes").ToArray() 
                : Array.Empty<Directive>();
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