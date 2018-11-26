namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using global::GraphQL.Types;
    using System.Threading.Tasks;
    using global::GraphQL.Language.AST;

    internal interface IFieldProcessor
    {
        Task<FieldRegistration> Process(
            Field field, 
            Identifier[] startIdentifiers, 
            ComplexGraphType<object> parent, 
            Dictionary<System.Type, GraphType> graphTypes);
    }
}