namespace EtAlii.Ubigia.Api.Functional.Querying
{
    public static class DataContext_QueryExtensions
    {
        public static IQueryContext CreateQueryContext(this IDataContext dataContext)
        {
            return new QueryContextFactory().Create(dataContext);
        }
    }
}