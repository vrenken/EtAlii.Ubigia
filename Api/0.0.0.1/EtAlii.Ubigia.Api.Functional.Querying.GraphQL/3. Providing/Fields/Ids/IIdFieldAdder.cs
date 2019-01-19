namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using global::GraphQL.Types;
    using System.Threading.Tasks;

    internal interface IIdFieldAdder
    {
        Task Add(
            string name,
            IdDirectiveResult idDirectiveResult, 
            FieldContext context, 
            GraphType parent,
            Dictionary<System.Type, GraphType> graphTypes);
    }
}