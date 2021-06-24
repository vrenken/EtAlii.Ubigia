// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingDataConnection : IProfilingDataConnection
    {
        public IProfiler Profiler { get; }

        public Storage Storage => _decoree.Storage;
        public Account Account => _decoree.Account;
        public Space Space => _decoree.Space;
        public IEntryContext Entries => _decoree.Entries;
        public IRootContext Roots => _decoree.Roots;
        public IContentContext Content => _decoree.Content;
        public IPropertiesContext Properties => _decoree.Properties;
        public bool IsConnected => _decoree.IsConnected;
        public IDataConnectionConfiguration Configuration => _decoree.Configuration;

        private readonly IDataConnection _decoree;

        public ProfilingDataConnection(IDataConnection decoree, IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler;
        }

        public async Task Open()
        {
            await _decoree.Open().ConfigureAwait(false);
        }

        public async Task Close()
        {
            await _decoree.Close().ConfigureAwait(false);
        }
    }
}
