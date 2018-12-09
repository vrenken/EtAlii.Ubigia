namespace EtAlii.Ubigia.Api.Functional
{
    internal class DataContext : IDataContext
    {
        public IDataContextConfiguration Configuration { get; }

        internal DataContext(IDataContextConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
