namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using global::GraphQL.Types;

    /// <example>
    /// This is an example JSON request for a mutation
    /// {
    ///   "query": "mutation ($human:HumanInput!){ createHuman(human: $human) { id name } }",
    ///   "variables": {
    ///     "human": {
    ///       "name": "Boba Fett"
    ///     }
    ///   }
    /// }
    /// </example>
    public class StaticMutation : ObjectGraphType<object>, IStaticMutation
    {
        public StaticMutation()//IUbigiaData data)
        {
            Name = "Mutation";

//            Field<HumanType>(
//                name: "createHuman",
//                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<HumanInputType>> {Name = "human"}),
//                resolve: context =>
//                {
//                    var human = context.GetArgument<Human>("human");
//                    return data.AddHuman(human);
//                });
        }
    }
}
