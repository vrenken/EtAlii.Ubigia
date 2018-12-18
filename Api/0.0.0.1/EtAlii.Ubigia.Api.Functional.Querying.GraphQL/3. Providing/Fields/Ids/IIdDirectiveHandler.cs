
namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Threading.Tasks;
    using global::GraphQL.Language.AST;

    internal interface IIdDirectiveHandler
    {
        Task<IdDirectiveResult> Handle(Directive directive, Identifier[] startIdentifiers);
    }
}