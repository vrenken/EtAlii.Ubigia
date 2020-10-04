namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GraphQL.Language.AST;
    using GraphQL.Types;

    internal class FieldProcessor : IFieldProcessor
    {
        private readonly INodesDirectiveHandler _nodesDirectiveHandler;
        private readonly IIdDirectiveHandler _idDirectiveHandler;
        private readonly INodesFieldAdder _nodesFieldAdder;
        private readonly IIdFieldAdder _idFieldAdder;

        public FieldProcessor(
            INodesDirectiveHandler nodesDirectiveHandler, 
            INodesFieldAdder nodesFieldAdder, 
            IIdDirectiveHandler idDirectiveHandler, 
            IIdFieldAdder idFieldAdder)
        {
            _nodesDirectiveHandler = nodesDirectiveHandler;
            _nodesFieldAdder = nodesFieldAdder;
            _idDirectiveHandler = idDirectiveHandler;
            _idFieldAdder = idFieldAdder;
        }

        public async Task<FieldContext> Process(
            Field field, 
            Context parentContext, 
            Dictionary<System.Type, GraphType> graphTypes)
        {
            FieldContext context = null;

            var parent = parentContext.GraphType; 

            var startIdentifiers = parentContext.NodesDirectiveResults
                .SelectMany(directive => directive.Nodes)
                .Select(node => node.Id)
                .ToArray();

            var nodesDirectiveResults = new List<NodesDirectiveResult>();
            var nodesDirectives = field.Directives
                .Where(directive => directive.Name == "nodes")
                .ToArray();
            foreach (var nodesDirective in nodesDirectives)
            {
                var directiveResult = await _nodesDirectiveHandler.Handle(nodesDirective, startIdentifiers);
                nodesDirectiveResults.Add(directiveResult);
            }
            
            var idDirectiveResults = new List<IdDirectiveResult>();
            var idDirectives = field.Directives
                .Where(directive => directive.Name == "id")
                .ToArray();
            foreach (var idDirective in idDirectives)
            {
                var idDirectiveResult = await _idDirectiveHandler.Handle(idDirective, startIdentifiers);
                idDirectiveResults.Add(idDirectiveResult);
            }
            var hasNodesDirectives = nodesDirectiveResults.Any();
            var hasIdDirectives = idDirectiveResults.Any();
            
            if (hasNodesDirectives && hasIdDirectives)
            {
                throw new NotSupportedException($"Nodes and id directives cannot be combined on the same field");    
            }
            
            if (hasNodesDirectives)
            {
                var results = nodesDirectiveResults.ToArray();
                context = FieldContext.FromDirectives(results);
                _nodesFieldAdder.Add(field.Name, results, context, parent, graphTypes);
            }
            else if (hasIdDirectives)
            {
                var result = idDirectiveResults.Single();
                context = FieldContext.FromDirectives(Array.Empty<NodesDirectiveResult>());
                await _idFieldAdder.Add(field.Name, result, context, parent, graphTypes);
            }

            return context;
        }
    }
}