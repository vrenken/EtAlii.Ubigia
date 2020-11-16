namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using GraphQL;
    using GraphQL.Resolvers;

    public class InstanceFieldResolver : IFieldResolver<object>
    {
        private readonly object _instance;

        public InstanceFieldResolver(object instance)
        {
            _instance = instance;
        }

        public object Resolve(IResolveFieldContext context)
        {
            return _instance;
        }

        object IFieldResolver.Resolve(IResolveFieldContext context)
        {
            return Resolve(context);
        }
    }
}