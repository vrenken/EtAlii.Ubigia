namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Threading.Tasks;
    using GraphQL.Language.AST;

    internal interface IFieldProcessor
    {
        Task<FieldContext> Process(
            Field field, 
            Context parentContext,
            IGraphTypeServiceProvider graphTypes);
    }
}