namespace EtAlii.Ubigia.Api.Functional.Querying
{
    public static class DataContextLinqQueryExtensions
    {
        public static ILinqQueryContext CreateLinqQueryContext(this IDataContext dataContext)
        {
            return new LinqQueryContextFactory().Create(dataContext);
        }
    }
}