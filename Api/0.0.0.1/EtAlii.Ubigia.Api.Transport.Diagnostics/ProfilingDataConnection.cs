namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    public class ProfilingDataConnection : IProfilingDataConnection 
    {
        public IProfiler Profiler { get { return _profiler; } }
        private readonly IProfiler _profiler;

        public Storage Storage { get { return _decoree.Storage; } }
        public Account Account { get { return _decoree.Account; } }
        public Space Space { get { return _decoree.Space; } }
        public IEntryContext Entries { get { return _decoree.Entries; } }
        public IRootContext Roots { get { return _decoree.Roots; } }
        public IContentContext Content { get { return _decoree.Content; } }
        public IPropertyContext Properties { get { return _decoree.Properties; } }
        public bool IsConnected { get { return _decoree.IsConnected; } }
        public IDataConnectionConfiguration Configuration { get { return _decoree.Configuration; } }

        private readonly IDataConnection _decoree;

        public ProfilingDataConnection(IDataConnection decoree, IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler;
        }

        public async Task Open()
        {
            await _decoree.Open();
        }

        public async Task Close()
        {
            await _decoree.Close();
        }
    }
}