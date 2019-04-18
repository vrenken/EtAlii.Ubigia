namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using global::GraphQL.Types;

    public class UbigiaQuery : ObjectGraphType<object>, IObjectGraphType, IComplexGraphType, IGraphType, IProvideMetadata, INamedType, IImplementInterfaces
    {
        public UbigiaQuery()
        { 
            Name = "Query";
//
//            Field<CharacterInterface>(
//                name: "hero",
//                resolve: context => data.GetDroidByIdAsync("3"))
//            
//            FieldDelegate<PersonType>(
//                name: "person",
//                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the person" }),
//                resolve: new Func<ResolveFieldContext, string, object>((context, id) => data.GetPersonByIdAsync(id))
//            )
//                
//            Field<PersonType>(
//                name: "person",
//                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the person" }),
//                resolve: context => data.GetPersonByIdAsync(context.GetArgument<string>("id"))
//            )
//            
//            Field<HumanType>(
//                name: "human",
//                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the human" }),
//                resolve: context => data.GetHumanByIdAsync(context.GetArgument<string>("id"))
//            )
//
//            Field<DroidType>(
//                name: "droid",
//                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the droid" }),
//                resolve: context => data.GetDroidByIdAsync(context.GetArgument<string>("id"))
//            )
//
//            FieldDelegate<DroidType>(
//                name: "droid",
//                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the droid" }),
//                resolve: new Func<ResolveFieldContext, string, object>((context, id) => data.GetDroidByIdAsync(id))
//            )
        }
    }
}
