namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using GraphQL.Types;

    public class UbigiaIdDirective : DirectiveGraphType
    {
        public UbigiaIdDirective()
            : base("id", new[]
            {
                DirectiveLocation.Field,
                DirectiveLocation.FragmentSpread,
                DirectiveLocation.InlineFragment,
            })
        {
            Description = "Directs the executor to identify a specific node in a space";
            Arguments = new QueryArguments(new QueryArgument<StringGraphType>
            {
                Name = "path",
                Description = "The path that should be queried and return the id."
            });
        }
    }

}
