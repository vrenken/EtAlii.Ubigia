namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GraphQL.Language.AST;
    using GraphQL.Types;

    internal interface IFieldProcessor
    {
        Task<FieldContext> Process(
            Field field, 
            Context parentContext, 
            Dictionary<System.Type, GraphType> graphTypes);
    }
}