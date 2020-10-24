namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using GraphQL.Types;

    public class UbigiaNodesDirective : DirectiveGraphType
    {
        public UbigiaNodesDirective()
            : base("nodes", new[]
            {
                DirectiveLocation.Query,
                DirectiveLocation.Field,
                DirectiveLocation.FragmentSpread,
                DirectiveLocation.InlineFragment,
            })
        {
            Description = "Directs the executor to start querying at specific nodes in a space";
            Arguments = new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = "path",
                Description = "The path at which to start the nodes query."
            });
        }
    }

}
