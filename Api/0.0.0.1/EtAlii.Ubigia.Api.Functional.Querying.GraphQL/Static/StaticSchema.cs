namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using global::GraphQL;
    using global::GraphQL.Types;

    public class StaticSchema : Schema
    {
        public new IDependencyResolver DependencyResolver => base.DependencyResolver;

        public StaticSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<StaticQuery>();
            Mutation = resolver.Resolve<StaticMutation>();

            RegisterDirectives(new DirectiveGraphType[]
            {
                new UbigiaStartDirective(),
            });
        }
    }
}
