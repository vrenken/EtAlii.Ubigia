// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using IContentContext = EtAlii.Ubigia.Api.Fabric.IContentContext;
    using IEntryContext = EtAlii.Ubigia.Api.Fabric.IEntryContext;
    using IRootContext = EtAlii.Ubigia.Api.Fabric.IRootContext;

    public class ProfilingFabricContext : IProfilingFabricContext
    {
        private readonly IFabricContext _decoree;
        public IFabricContextConfiguration Configuration => _decoree.Configuration;
        public IDataConnection Connection => _decoree.Connection;
        public IRootContext Roots => _decoree.Roots;
        public IEntryContext Entries => _decoree.Entries;
        public IContentContext Content => _decoree.Content;
        public Fabric.IPropertiesContext Properties => _decoree.Properties;

        public IProfiler Profiler { get; }

        public ProfilingFabricContext(
            IFabricContext decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            if (disposing)
            {
                _decoree.Dispose();
            }
        }

        ~ProfilingFabricContext()
        {
            Dispose(false);
        }
    }
}