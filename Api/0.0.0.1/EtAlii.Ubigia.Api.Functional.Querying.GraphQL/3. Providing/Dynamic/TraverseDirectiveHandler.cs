namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;

    internal class TraverseDirectiveHandler : ITraverseDirectiveHandler
    {
        private readonly IFieldTypeBuilder _fieldTypeBuilder;
        private readonly INodeFetcher _nodeFetcher;

        public TraverseDirectiveHandler(IFieldTypeBuilder fieldTypeBuilder, INodeFetcher nodeFetcher)
        {
            _fieldTypeBuilder = fieldTypeBuilder;
            _nodeFetcher = nodeFetcher;
        }

        public async Task<TraverseDirective> Handle(Directive directive, string name, IObjectGraphType query, Dictionary<System.Type, DynamicObjectGraphType> graphObjectInstances)
        {
            var result = new TraverseDirective();
            
            var argument = directive.Arguments.First();
            if (argument.Value is StringValue stringValue)
            {
                var path = stringValue.Value;
                result.Nodes = await _nodeFetcher.FetchAsync(path);
                result.Path = path;
                
                var properties = result.Nodes.First().GetProperties();
                _fieldTypeBuilder.Build(path, name, properties, out DynamicObjectGraphType fieldTypeInstance, out FieldType fieldType);

                result.FieldTypeInstance = fieldTypeInstance;
                result.FieldType = fieldType;
                
                graphObjectInstances[result.FieldTypeInstance.GetType()] = result.FieldTypeInstance;
                
                ((ComplexGraphType<object>)query).AddField(result.FieldType);
            }
            
            return result;
        }
    }
}