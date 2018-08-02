namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using global::GraphQL.Types;

    public class StaticQuery : ObjectGraphType<object>, IStaticQuery
    {
        public StaticQuery(IUbigiaData data)
        {
            Name = "Query";

            Field<CharacterInterface>(
                name: "hero",
                resolve: context => data.GetDroidByIdAsync("3"));
            Field<HumanType>(
                name: "human",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the human" }),
                resolve: context => data.GetHumanByIdAsync(context.GetArgument<string>("id"))
            );

            FieldDelegate<DroidType>(
                name: "droid",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the droid" }),
                resolve: new Func<ResolveFieldContext, string, object>((context, id) => data.GetDroidByIdAsync(id))
            );
        }
    }
}
