namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using global::GraphQL.Types;

    class IdFieldAdder : IIdFieldAdder
    {
        private readonly IFieldTypeBuilder _fieldTypeBuilder;

        public IdFieldAdder(IFieldTypeBuilder fieldTypeBuilder)
        {
            _fieldTypeBuilder = fieldTypeBuilder;
        }

        public void Add(
            string name,
            IdDirectiveResult idDirectiveResult, 
            Registration registration, 
            ComplexGraphType<object> parent, 
            Dictionary<System.Type, GraphType> graphObjectInstances)
        {
        
            var id = idDirectiveResult.Id;
            _fieldTypeBuilder.Build(idDirectiveResult.Path, name, id, out ScalarGraphType fieldTypeInstance, out FieldType fieldType);

            registration.FieldTypeInstance = fieldTypeInstance;
            registration.FieldType = fieldType;
            
            graphObjectInstances[fieldTypeInstance.GetType()] = fieldTypeInstance;
            
            parent.AddField(fieldType);
        }
    }
}