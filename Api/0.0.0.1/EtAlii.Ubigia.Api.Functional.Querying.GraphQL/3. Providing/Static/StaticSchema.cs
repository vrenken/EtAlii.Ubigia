namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using global::GraphQL;
    using global::GraphQL.Types;

    public class StaticSchema : Schema, IStaticSchema
    {
        public new IDependencyResolver DependencyResolver => base.DependencyResolver;

        public StaticSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<IStaticQuery>();
            Mutation = resolver.Resolve<IStaticMutation>();
 
            RegisterDirectives(new UbigiaNodesDirective());
            RegisterDirectives(new UbigiaIdDirective());
        }
    }
}
