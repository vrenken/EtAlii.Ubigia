namespace EtAlii.Ubigia.Api.Functional
{
    public static class DataContextGraphSLExtensions
    {
        public static IGraphSLScriptContext CreateGraphSLScriptContext(this IDataContext dataContext, IGraphSLScriptContextConfiguration configuration = null)
        {
            configuration = configuration ?? new GraphSLScriptContextConfiguration();
            configuration = configuration.Use(dataContext);
            return new GraphSLScriptContextFactory().Create(configuration);
        }
    }
}