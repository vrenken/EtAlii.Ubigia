namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using global::GraphQL.Types;

    class IdFieldAdder : IIdFieldAdder
    {
        private readonly IGraphTypeBuilder _graphTypeBuilder;

        public IdFieldAdder(IGraphTypeBuilder graphTypeBuilder)
        {
            _graphTypeBuilder = graphTypeBuilder;
        }

        public void Add(
            string name,
            IdDirectiveResult idDirectiveResult, 
            Registration registration,
            ComplexGraphType<object> parent, 
            Dictionary<System.Type, GraphType> graphTypes)
        {
        
            var id = idDirectiveResult.Id.SingleOrDefault();
            if (id != null)
            {
                _graphTypeBuilder.Build(idDirectiveResult.Path, name, id, out ScalarGraphType graphType, out FieldType fieldType);

                registration.GraphType = graphType;
                registration.FieldType = fieldType;

                graphTypes[graphType.GetType()] = graphType;

                parent.AddField(fieldType);
            }
        }
    }
}