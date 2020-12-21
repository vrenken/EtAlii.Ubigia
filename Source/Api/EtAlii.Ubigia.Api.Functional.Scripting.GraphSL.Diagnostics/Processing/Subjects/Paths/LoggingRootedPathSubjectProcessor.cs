namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Threading.Tasks;
    using Serilog;

    internal class LoggingRootedPathSubjectProcessor : IRootedPathSubjectProcessor
    {
        private readonly IRootedPathSubjectProcessor _decoree;
        private readonly ILogger _logger = Log.ForContext<IRootedPathSubjectProcessor>();

        public LoggingRootedPathSubjectProcessor(IRootedPathSubjectProcessor decoree)
        {
            _decoree = decoree;
        }

        public async Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            _logger.Information("Processing rooted path {PathSubject}", subject);
            var start = Environment.TickCount;

            await _decoree.Process(subject, scope, output).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Processed rooted path (Duration: {Duration}ms)", duration);
        }
    }
}
