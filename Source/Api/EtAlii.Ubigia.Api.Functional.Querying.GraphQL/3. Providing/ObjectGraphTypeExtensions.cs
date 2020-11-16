namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using GraphQL;
    using GraphQL.Resolvers;
    using GraphQL.Types;

    public static class ObjectGraphTypeExtensions
    {
        public static void Field(
            this IObjectGraphType obj,
            string name,
            IGraphType type,
            string description = null,
            QueryArguments arguments = null,
            Func<IResolveFieldContext, object> resolve = null)
        {
            var field = new FieldType
            {
                Name = name,
                Description = description,
                Arguments = arguments,
                ResolvedType = type,
                Resolver = resolve != null ? new FuncFieldResolver<object>(resolve) : null
            };
            obj.AddField(field);
        }
    }
}