namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Threading.Tasks;
    using GraphQL.Types;

    internal interface IIdFieldAdder
    {
        Task Add(
            string name,
            IdDirectiveResult idDirectiveResult, 
            FieldContext context, 
            GraphType parent,
            IGraphTypeServiceProvider graphTypes);
    }
}