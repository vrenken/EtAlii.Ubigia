namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using global::GraphQL.Types;

    class IdFieldAdder : IIdFieldAdder
    {
        private readonly IScalarFieldTypeBuilder _scalarFieldTypeBuilder;

        public IdFieldAdder(IScalarFieldTypeBuilder scalarFieldTypeBuilder)
        {
            _scalarFieldTypeBuilder = scalarFieldTypeBuilder;
        }

        public void Add(
            string name,
            IdDirectiveResult idDirectiveResult, 
            Registration registration,
            GraphType parent,
            Dictionary<System.Type, GraphType> graphTypes)
        {
        
            var id = idDirectiveResult.Id.SingleOrDefault();
            if (id != null)
            {
                var fieldType = _scalarFieldTypeBuilder.Build(idDirectiveResult.Path, name, id, graphTypes, out var graphType);
                registration.GraphType = graphType;
                ((ComplexGraphType<object>)parent).AddField(fieldType);
            }
        }
    }
}