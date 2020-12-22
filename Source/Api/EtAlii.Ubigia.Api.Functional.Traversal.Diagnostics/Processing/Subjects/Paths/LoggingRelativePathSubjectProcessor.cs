namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Serilog;
    using System;
    using System.Threading.Tasks;

    internal class LoggingRelativePathSubjectProcessor : IRelativePathSubjectProcessor
    {
        private readonly IRelativePathSubjectProcessor _decoree;
        private readonly ILogger _logger = Log.ForContext<IRelativePathSubjectProcessor>();

        public LoggingRelativePathSubjectProcessor(IRelativePathSubjectProcessor decoree)
        {
            _decoree = decoree;
        }

        public async Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            _logger.Information("Processing relative path {PathSubject}", subject);
            var start = Environment.TickCount;

            await _decoree.Process(subject, scope, output).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Processed relative path (Duration: {Duration}ms)", duration);
        }
    }
}
