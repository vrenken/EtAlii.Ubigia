namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Threading.Tasks;
    using global::GraphQL.Language.AST;

    internal interface INodesDirectiveHandler
    {
        Task<NodesDirective> Handle(Directive directive, Identifier[] startIdentifiers = null);
    }
}