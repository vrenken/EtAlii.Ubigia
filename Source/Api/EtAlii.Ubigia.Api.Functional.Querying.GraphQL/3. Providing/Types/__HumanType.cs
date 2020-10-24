//using GraphQL.Types
//
//namespace EtAlii.Ubigia.Api.Functional.Querying
//[
//    public class HumanType : ObjectGraphType<Human> 
//    [
//        public HumanType(IUbigiaData data)
//        [
//            Name = "Human"
//
//            Field(h => h.Id).Description("The id of the human.")
//            Field(h => h.Name, nullable: true).Description("The name of the human.")
//
//            Field<ListGraphType<CharacterInterface>>(
//                "friends",
//                resolve: context => data.GetFriends(context.Source)
//            )
//            Field<ListGraphType<EpisodeEnum>>("appearsIn", "Which movie they appear in.")
//
//            Field(h => h.HomePlanet, nullable: true).Description("The home planet of the human.")
//
//            Interface<CharacterInterface>()
//        ]
//    ]
//]