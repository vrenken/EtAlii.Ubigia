namespace EtAlii.Ubigia.Provisioning.Twitter
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using Serilog;

    public class TweetImporter : ITweetImporter
    {
        private IDisposable _subscription;
        private readonly ILogger _logger = Log.ForContext<ITweetImporter>();

        public Task Start()
        {
            _logger.Information("Starting Twitter provider");

            _subscription = Observable.Interval(TimeSpan.FromSeconds(60))
                                      .Subscribe(Add);

            _logger.Information("Started Twitter provider");
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            _logger.Information("Stopping Twitter provider");

            _subscription.Dispose();
            _subscription = null;

            _logger.Information("Stopped Twitter provider");
            return Task.CompletedTask;
        }

        private void Add(long time)
        {
            _logger.Information("Checking Twitter for new content");
        }
    }
}