namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Transport;
    using IContentContext = EtAlii.Ubigia.Api.Fabric.IContentContext;
    using IEntryContext = EtAlii.Ubigia.Api.Fabric.IEntryContext;
    using IPropertyContext = EtAlii.Ubigia.Api.Fabric.IPropertyContext;
    using IRootContext = EtAlii.Ubigia.Api.Fabric.IRootContext;

    public class ProfilingFabricContext : IProfilingFabricContext
    {
        private readonly IFabricContext _decoree;
        public IFabricContextConfiguration Configuration => _decoree.Configuration;
        public IDataConnection Connection => _decoree.Connection;
        public IRootContext Roots => _decoree.Roots;
        public IEntryContext Entries => _decoree.Entries;
        public IContentContext Content => _decoree.Content;
        public IPropertyContext Properties => _decoree.Properties;

        public IProfiler Profiler => _profiler;
        private readonly IProfiler _profiler;

        public ProfilingFabricContext(
            IFabricContext decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler;
        }

        public void Dispose()
        {
            _decoree.Dispose();
        }
    }
}