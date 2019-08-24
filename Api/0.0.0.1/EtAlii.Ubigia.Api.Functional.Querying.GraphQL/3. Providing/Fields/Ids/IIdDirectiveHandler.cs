
namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Threading.Tasks;
    using GraphQL.Language.AST;

    internal interface IIdDirectiveHandler
    {
        Task<IdDirectiveResult> Handle(Directive directive, Identifier[] startIdentifiers);
    }
}