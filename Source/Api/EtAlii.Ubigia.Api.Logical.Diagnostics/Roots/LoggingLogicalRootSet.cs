// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LoggingLogicalRootSet : ILogicalRootSet
    {
        private readonly ILogicalRootSet _decoree;
        private readonly ILogger _logger = Log.ForContext<ILogicalRootSet>();

        public LoggingLogicalRootSet(ILogicalRootSet decoree)
        {
            _decoree = decoree;
        }

        public async Task<Root> Add(string name)
        {
            _logger.Debug("Adding root {RootName}", name);
            var start = Environment.TickCount;

            var result = await _decoree.Add(name).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Added root {@RootName} (Duration: {Duration}ms)", result, duration);

            return result;
        }

        public async Task Remove(Guid id)
        {
            _logger.Debug("Removing root {RootId}", id);
            var start = Environment.TickCount;

            await _decoree.Remove(id).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Removing root (Duration: {Duration}ms)", duration);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            _logger.Debug("Changing root {RootId} {RootName}", rootId, rootName);
            var start = Environment.TickCount;

            var result =await _decoree.Change(rootId, rootName).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Changing root (Duration: {Duration}ms)", duration);

            return result;
        }

        public async Task<Root> Get(string rootName)
        {
            _logger.Debug("Getting root {RootName}", rootName);
            var start = Environment.TickCount;

            var result = await _decoree.Get(rootName).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Got root (Duration: {Duration}ms)", duration);

            return result;
        }

        public async Task<Root> Get(Guid rootId)
        {
            _logger.Debug("Getting root {RootId}", rootId);
            var start = Environment.TickCount;

            var result = await _decoree.Get(rootId).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Got root (Duration: {Duration}ms)", duration);

            return result;
        }

        public async IAsyncEnumerable<Root> GetAll()
        {
            _logger.Debug("Getting all roots");
            var start = Environment.TickCount;

            var items = _decoree
                .GetAll()
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                yield return item;
            }

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Got all roots (Duration: {Duration}ms)", duration);
        }
    }
}
