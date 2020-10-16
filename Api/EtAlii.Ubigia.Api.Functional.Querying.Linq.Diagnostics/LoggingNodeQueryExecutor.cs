namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Diagnostics;
    using Remotion.Linq;
    using Serilog;

    internal class LoggingNodeQueryExecutor : INodeQueryExecutor
    {
        private readonly INodeQueryExecutor _decoree;
        private readonly ILogger _logger = Log.ForContext<INodeQueryExecutor>();

        public LoggingNodeQueryExecutor(INodeQueryExecutor decoree)
        {
            _decoree = decoree;
        }

        public T ExecuteScalar<T>(QueryModel queryModel)
        {
            // We want to be able to track method calls throughout the whole application stack.
            // Including across network and process boundaries.
            // For this we create a unique correlationId and pass it through all involved systems.
            using (ContextCorrelator.BeginCorrelationScope("CorrelationId", Guid.NewGuid(), false))
            {
                var message = "Executing scalar<{type}> node Linq query transformation";
                _logger.Information(message, nameof(T));
                var start = Environment.TickCount;

                var result = _decoree.ExecuteScalar<T>(queryModel);
                
                var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
                _logger.Information("Executing scalar<{type}> node Linq query transformation (Duration: {duration}ms)", nameof(T), duration);

                return result;
            }
        }

        public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
        {
            // We want to be able to track method calls throughout the whole application stack.
            // Including across network and process boundaries.
            // For this we create a unique correlationId and pass it through all involved systems.
            using (ContextCorrelator.BeginCorrelationScope("CorrelationId", Guid.NewGuid(), false))
            {
                var message = "Executing single<{type}> node Linq query transformation";
                _logger.Information(message, nameof(T));
                var start = Environment.TickCount;

                var result = _decoree.ExecuteSingle<T>(queryModel, returnDefaultWhenEmpty);
                
                var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
                _logger.Information("Executing single<{type}> node Linq query transformation (Duration: {duration}ms)", nameof(T), duration);
                
                return result;
            }
        }

        public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
        {
            // We want to be able to track method calls throughout the whole application stack.
            // Including across network and process boundaries.
            // For this we create a unique correlationId and pass it through all involved systems.
            using (ContextCorrelator.BeginCorrelationScope("CorrelationId", Guid.NewGuid(), false))
            {
                var message = "Executing collection<{type}> node Linq query transformation";
                _logger.Information(message, nameof(T));
                var start = Environment.TickCount;

                var result = _decoree.ExecuteCollection<T>(queryModel);
                
                var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
                _logger.Information("Executing collection<{type}> node Linq query transformation (Duration: {duration}ms)", nameof(T), duration);
                
                return result;
            }
        }
    }
}