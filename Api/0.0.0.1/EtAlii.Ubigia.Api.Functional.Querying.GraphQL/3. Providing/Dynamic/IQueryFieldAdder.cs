namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using global::GraphQL.Types;
    using System.Threading.Tasks;
    using global::GraphQL.Language.AST;

    internal interface IQueryFieldAdder
    {
        void Add(
            string name,
            IEnumerable<NodesDirective> directives, 
            Registration registration, 
            IObjectGraphType query, 
            Dictionary<System.Type, DynamicObjectGraphType> graphObjectInstances);
    }
}