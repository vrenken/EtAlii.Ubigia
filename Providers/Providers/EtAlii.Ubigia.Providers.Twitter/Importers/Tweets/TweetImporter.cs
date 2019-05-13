namespace EtAlii.Ubigia.Provisioning.Twitter
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Logging;

    public class TweetImporter : ITweetImporter
    {
        private IDisposable _subscription;
        private readonly ILogger _logger;

        public TweetImporter(ILogger logger)
        {
            _logger = logger;
        }

        public Task Start()
        {
            _logger.Info("Starting Twitter provider");

            _subscription = Observable.Interval(TimeSpan.FromSeconds(60))
                                      .Subscribe(Add);

            _logger.Info("Started Twitter provider");
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            _logger.Info("Stopping Twitter provider");

            _subscription.Dispose();
            _subscription = null;

            _logger.Info("Stopped Twitter provider");
            return Task.CompletedTask;
        }

        private void Add(long time)
        {
            _logger.Info("Checking Twitter for new content");
        }
    }
}