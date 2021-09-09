// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingDataConnection : IProfilingDataConnection
    {
        public IProfiler Profiler { get; }

        /// <inheritdoc />
        public Storage Storage => _decoree.Storage;

        /// <inheritdoc />
        public Account Account => _decoree.Account;

        /// <inheritdoc />
        public Space Space => _decoree.Space;

        /// <inheritdoc />
        public IEntryContext Entries => _decoree.Entries;

        /// <inheritdoc />
        public IRootContext Roots => _decoree.Roots;

        /// <inheritdoc />
        public IContentContext Content => _decoree.Content;

        /// <inheritdoc />
        public IPropertiesContext Properties => _decoree.Properties;

        /// <inheritdoc />
        public bool IsConnected => _decoree.IsConnected;

        /// <inheritdoc />
        public DataConnectionOptions Options => _decoree.Options;

        private readonly IDataConnection _decoree;

        public ProfilingDataConnection(IDataConnection decoree, IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler;
        }

        /// <inheritdoc />
        public async Task Open()
        {
            await _decoree.Open().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task Close()
        {
            await _decoree.Close().ConfigureAwait(false);
        }
    }
}
