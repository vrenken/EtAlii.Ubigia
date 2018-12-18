namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using global::GraphQL.Resolvers;
    using global::GraphQL.Types;

    public static class ObjectGraphTypeExtensions
    {
        public static void Field(
            this IObjectGraphType obj,
            string name,
            IGraphType type,
            string description = null,
            QueryArguments arguments = null,
            Func<ResolveFieldContext, object> resolve = null)
        {
            var field = new FieldType();
            field.Name = name;
            field.Description = description;
            field.Arguments = arguments;
            field.ResolvedType = type;
            field.Resolver = resolve != null ? new FuncFieldResolver<object>(resolve) : null;
            obj.AddField(field);
        }
    }
}