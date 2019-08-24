namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GraphQL.Language.AST;
    using GraphQL.Types;

    internal interface IOperationProcessor
    {
        Task<OperationContext> Process(
            Operation operation, 
            ComplexGraphType<object> query, 
            Dictionary<System.Type, GraphType> graphTypes);
    }
}