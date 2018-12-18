//namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
//{
//    using global::GraphQL;
//    using global::GraphQL.Types;
//
//    public class StaticSchema : Schema, ISchema
//    {
//        public new IDependencyResolver DependencyResolver => base.DependencyResolver;
//
//        public StaticSchema(IDependencyResolver resolver)
//            : base(resolver)
//        {
//            Query = new StaticQuery();
//            Mutation = new StaticMutation();
// 
//            RegisterDirectives(new UbigiaNodesDirective());
//            RegisterDirectives(new UbigiaIdDirective());
//        }
//    }
//}
