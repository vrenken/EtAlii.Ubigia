namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using GraphQL.Types;

    class IdFieldAdder : IIdFieldAdder
    {
        private readonly IScalarFieldTypeBuilder _scalarFieldTypeBuilder;
        private readonly INodeFetcher _nodeFetcher;

        public IdFieldAdder(
            IScalarFieldTypeBuilder scalarFieldTypeBuilder, 
            INodeFetcher nodeFetcher)
        {
            _scalarFieldTypeBuilder = scalarFieldTypeBuilder;
            _nodeFetcher = nodeFetcher;
        }

        public async Task Add(
            string name,
            IdDirectiveResult idDirectiveResult, 
            FieldContext context,
            GraphType parent,
            IGraphTypeServiceProvider graphTypes)
        {   
            if(parent.Metadata.TryGetValue(ComplexFieldTypeBuilder.NodeMetadataKey, out var node))
            {
                await AddIdToInstance(name, idDirectiveResult, context, parent, graphTypes, (IInternalNode)node).ConfigureAwait(false);
            }
            else if(parent.Metadata.TryGetValue(ListFieldTypeBuilder.DynamicObjectsMetadataKey, out var dynamicObjects))
            {
                AddIdToList(name, idDirectiveResult, parent, (IDictionary<Identifier, DynamicObjectTuple>)dynamicObjects);
            }
        }

        private async Task AddIdToInstance(
            string name, 
            IdDirectiveResult idDirectiveResult, 
            FieldContext context, 
            GraphType parent,
            IGraphTypeServiceProvider graphTypes, 
            IInternalNode node)
        {
            var idValue = await GetId(node.Id, idDirectiveResult.Path).ConfigureAwait(false);
            
            var fieldType = _scalarFieldTypeBuilder.Build(idDirectiveResult.Path, name, idValue, out var graphType);
            ((ComplexGraphType<object>) parent).AddField(fieldType);

            if (graphType != null)
            {
                context.GraphType = graphType;
                graphTypes.Register(graphType);
            }
        }

        private void AddIdToList(
            string name, 
            IdDirectiveResult idDirectiveResult, 
            GraphType parent, 
            IDictionary<Identifier, DynamicObjectTuple> dynamicObjects)
        {
            foreach (var dynamicObject in dynamicObjects.Values)
            {
                var idValue = GetId(dynamicObject.Identifier, idDirectiveResult.Path);
                
                var properties = dynamicObject.Properties;
                var clonedProperties = new PropertyDictionary(properties) {{name, idValue}};
                dynamicObject.Properties = clonedProperties;
                dynamicObject.Instance = DynamicObject.CreateInstance(clonedProperties);
            }

            var fieldType = _scalarFieldTypeBuilder.BuildShallow(idDirectiveResult.Path, name, string.Empty);

            var listGraphType = (ComplexGraphType<object>) ((ListGraphType) parent).ResolvedType;
            listGraphType.AddField(fieldType);
        }

        private async Task<string> GetId(Identifier startIdentifier, string path)
        {
            path = $"/&{startIdentifier.ToDotSeparatedString()}{path ?? string.Empty}";
            var subSet = await _nodeFetcher.FetchAsync(path).ConfigureAwait(false);
            return subSet.SingleOrDefault()?.Type;
        }
    }
}