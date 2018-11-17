namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using global::GraphQL.Types;

    public class UbigiaTraverseDirective : DirectiveGraphType
    {
        public UbigiaTraverseDirective()
            : base("traverse", new[]
            {
                DirectiveLocation.Query,
                DirectiveLocation.Field,
                DirectiveLocation.FragmentSpread,
                DirectiveLocation.InlineFragment,
            })
        {
            Description = "Directs the executor to start querying at a specific location in a space";
            Arguments = new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = "path",
                Description = "The path at wich to start the query."
            });
        }
    }

}
