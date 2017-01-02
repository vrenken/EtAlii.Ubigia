namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using IContentContext = EtAlii.Ubigia.Api.Fabric.IContentContext;
    using IEntryContext = EtAlii.Ubigia.Api.Fabric.IEntryContext;
    using IPropertyContext = EtAlii.Ubigia.Api.Fabric.IPropertyContext;
    using IRootContext = EtAlii.Ubigia.Api.Fabric.IRootContext;

    public class ProfilingFabricContext : IProfilingFabricContext
    {
        private readonly IFabricContext _decoree;
        public IFabricContextConfiguration Configuration { get { return _decoree.Configuration; } }
        public IDataConnection Connection { get { return _decoree.Connection; } }
        public IRootContext Roots { get { return _decoree.Roots; } }
        public IEntryContext Entries { get { return _decoree.Entries; } }
        public IContentContext Content { get { return _decoree.Content; } }
        public IPropertyContext Properties { get { return _decoree.Properties; } }

        public IProfiler Profiler { get { return _profiler; } }
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