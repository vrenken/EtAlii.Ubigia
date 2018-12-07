namespace EtAlii.Ubigia.Api.Functional
{
    internal class DataContext : IDataContext
    {
        public IScriptsSet Scripts { get; }

        public IDataContextConfiguration Configuration { get; }

        internal DataContext(
            IDataContextConfiguration configuration,
            IScriptsSet scripts)
        {
            Configuration = configuration;
            Scripts = scripts;
        }
    }
}
