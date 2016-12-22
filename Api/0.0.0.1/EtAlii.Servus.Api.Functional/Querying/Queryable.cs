namespace EtAlii.Servus.Api.Functional
{
    using System.Linq;
    using System.Linq.Expressions;
    using Remotion.Linq;

    public class Queryable<T> : QueryableBase<T>
    {
        protected internal Queryable(IQueryProvider queryProvider)
            : base(queryProvider)
        {
        }

        // This constructor is called indirectly by LINQ's query methods, just pass to base.
        internal Queryable(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }
    }
}