namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using global::GraphQL.Types;

    class QueryFieldAdder : IQueryFieldAdder
    {
        private readonly IFieldTypeBuilder _fieldTypeBuilder;

        public QueryFieldAdder(IFieldTypeBuilder fieldTypeBuilder)
        {
            _fieldTypeBuilder = fieldTypeBuilder;
        }

        public void Add(
            string name,
            IEnumerable<NodesDirective> directives, 
            Registration registration, 
            IObjectGraphType query, 
            Dictionary<System.Type, DynamicObjectGraphType> graphObjectInstances)
        {
            if (directives.FirstOrDefault() is NodesDirective result)
            {
                var properties = result.Nodes.First().GetProperties();
                _fieldTypeBuilder.Build(result.Path, name, properties, out DynamicObjectGraphType fieldTypeInstance, out FieldType fieldType);

                registration.FieldTypeInstance = fieldTypeInstance;
                registration.FieldType = fieldType;
                
                graphObjectInstances[fieldTypeInstance.GetType()] = fieldTypeInstance;
                
                ((ComplexGraphType<object>)query).AddField(fieldType);
            }
        }
    }
}