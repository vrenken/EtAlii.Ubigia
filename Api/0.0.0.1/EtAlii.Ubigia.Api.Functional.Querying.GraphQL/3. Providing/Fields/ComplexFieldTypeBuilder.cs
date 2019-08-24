﻿namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Api.Logical;
    using GraphQL.Types;

    internal class ComplexFieldTypeBuilder : IComplexFieldTypeBuilder
    {
        public const string NodeMetadataKey = "Node";

        public FieldType Build(
            string path,
            string name,
            IInternalNode node,
            out GraphType graphType)
        {
            var properties = node?.GetProperties() ?? new PropertyDictionary();
            graphType = DynamicObjectGraphType.Create(path, name, properties);
            graphType.Metadata[NodeMetadataKey] = node; 
            
            var result = new FieldType
            {
                Name = graphType.Name,
                Description = $"Field created for the Ubigia path: {path}",
                Type = graphType.GetType(),
                Arguments = null,
                Resolver = new InstanceFieldResolver(graphType)
            };
            return result;
        }        
    }
}
