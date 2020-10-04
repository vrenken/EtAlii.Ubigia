namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GraphQL.Types;

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