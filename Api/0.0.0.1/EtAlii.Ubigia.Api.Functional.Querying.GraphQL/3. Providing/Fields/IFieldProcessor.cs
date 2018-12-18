namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using global::GraphQL.Types;
    using System.Threading.Tasks;
    using global::GraphQL.Language.AST;

    internal interface IFieldProcessor
    {
        Task<FieldContext> Process(
            Field field, 
            Context parentContext, 
            Dictionary<System.Type, GraphType> graphTypes);
    }
}