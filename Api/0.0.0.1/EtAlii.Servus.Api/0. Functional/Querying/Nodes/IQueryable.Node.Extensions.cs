namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using EtAlii.Servus.Api.Logical;

    public static class IQueryableNodeExtensions
    {
        public static IQueryable<INode> Select(this IQueryable<INode> source, string path)
        {
            return source.Provider.CreateQuery<INode>(Expression.Call(NodeExtensionMethod.Select, source.Expression, Expression.Constant(path)));
        }

        public static IQueryable<INode> Latest(this IQueryable<INode> source)
        {
            return source.Provider.CreateQuery<INode>(Expression.Call(NodeExtensionMethod.Latest, source.Expression, Expression.Constant(String.Empty)));
        }

        public static IQueryable<INode> Add(this IQueryable<INode> source, string path)
        {
            return source.Provider.CreateQuery<INode>(Expression.Call(NodeExtensionMethod.Add, source.Expression, Expression.Constant(path)));
        }

        public static IQueryable<INode> At(this IQueryable<INode> source, DateTime dateTime)
        {
            return source.Provider.CreateQuery<INode>(Expression.Call(NodeExtensionMethod.At, source.Expression, Expression.Constant(dateTime)));
        }

    }
}