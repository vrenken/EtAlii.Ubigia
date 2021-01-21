namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using EtAlii.xTechnology.Diagnostics;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Threading;
    using Remotion.Linq;
    using Serilog;

    internal class LoggingNodeQueryExecutor : INodeQueryExecutor
    {
        private readonly INodeQueryExecutor _decoree;
        private readonly IContextCorrelator _contextCorrelator;
        private readonly ILogger _logger = Log.ForContext<INodeQueryExecutor>();

        public LoggingNodeQueryExecutor(INodeQueryExecutor decoree, IContextCorrelator contextCorrelator)
        {
            _decoree = decoree;
            _contextCorrelator = contextCorrelator;
        }

        public T ExecuteScalar<T>(QueryModel queryModel)
        {
            // We want to be able to track method calls throughout the whole application stack.
            // Including across network and process boundaries.
            // For this we create a unique correlationId and pass it through all involved systems.
            using (_contextCorrelator.BeginLoggingCorrelationScope(Correlation.ScriptId, ShortGuid.New(), false))
            {
                _logger.Information("Executing scalar<{Type}> node Linq query transformation", nameof(T));
                var start = Environment.TickCount;

                var result = _decoree.ExecuteScalar<T>(queryModel);

                var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
                _logger.Information("Executing scalar<{Type}> node Linq query transformation (Duration: {Duration}ms)", nameof(T), duration);

                return result;
            }
        }

        public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
        {
            // We want to be able to track method calls throughout the whole application stack.
            // Including across network and process boundaries.
            // For this we create a unique correlationId and pass it through all involved systems.
            using (_contextCorrelator.BeginLoggingCorrelationScope(Correlation.ScriptId, ShortGuid.New(), false))
            {
                _logger.Information("Executing single<{Type}> node Linq query transformation", nameof(T));
                var start = Environment.TickCount;

                var result = _decoree.ExecuteSingle<T>(queryModel, returnDefaultWhenEmpty);

                var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
                _logger.Information("Executing single<{Type}> node Linq query transformation (Duration: {Duration}ms)", nameof(T), duration);

                return result;
            }
        }

        public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
        {
            // We want to be able to track method calls throughout the whole application stack.
            // Including across network and process boundaries.
            // For this we create a unique correlationId and pass it through all involved systems.
            using (_contextCorrelator.BeginLoggingCorrelationScope(Correlation.ScriptId, ShortGuid.New(), false))
            {
                _logger.Information("Executing collection<{Type}> node Linq query transformation", nameof(T));
                var start = Environment.TickCount;

                var result = _decoree.ExecuteCollection<T>(queryModel);

                var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
                _logger.Information("Executing collection<{Type}> node Linq query transformation (Duration: {Duration}ms)", nameof(T), duration);

                return result;
            }
        }
    }
}
