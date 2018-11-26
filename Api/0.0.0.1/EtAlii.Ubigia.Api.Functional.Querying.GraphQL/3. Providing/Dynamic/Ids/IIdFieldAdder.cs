namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using global::GraphQL.Types;
    using System.Threading.Tasks;
    using global::GraphQL.Language.AST;

    internal interface IIdFieldAdder
    {
        void Add(
            string name,
            IdDirectiveResult idDirectiveResult, 
            Registration registration, 
            ComplexGraphType<object> parent, 
            Dictionary<System.Type, GraphType> graphTypes);
    }
}