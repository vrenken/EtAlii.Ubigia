namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using global::GraphQL.Types;
    using System.Threading.Tasks;
    using global::GraphQL.Language.AST;

    internal interface IOperationProcessor
    {
        Task<OperationContext> Process(
            Operation operation, 
            ComplexGraphType<object> query, 
            Dictionary<System.Type, GraphType> graphTypes);
    }
}