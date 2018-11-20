namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using global::GraphQL.Types;
    using System.Threading.Tasks;
    using global::GraphQL.Language.AST;

    internal interface ITraverseDirectiveHandler
    {
        Task<TraverseDirective> Handle(Directive directive, IObjectGraphType query, Dictionary<System.Type, DynamicObjectGraphType> graphObjectInstances);
    }
}