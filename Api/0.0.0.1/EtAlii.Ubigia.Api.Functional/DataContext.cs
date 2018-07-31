namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class DataContext : IDataContext
    {
        public IScriptsSet Scripts { get; }
        public IQuerySet Queries { get; }

        public IDataContextConfiguration Configuration { get; }

        internal DataContext(
            IDataContextConfiguration configuration,
            IScriptsSet scripts,
            IQuerySet querySet)
        {
            Configuration = configuration;
            Scripts = scripts;
            Queries = querySet;
        }
    }
}
