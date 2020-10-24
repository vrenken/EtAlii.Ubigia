namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Threading.Tasks;
    using GraphQL.Language.AST;

    internal interface INodesDirectiveHandler
    {
        Task<NodesDirectiveResult> Handle(Directive directive, Identifier[] startIdentifiers = null);
    }
}