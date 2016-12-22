namespace EtAlii.Servus.Provisioning.Twitter
{
    using EtAlii.Servus.Provisioning;
    using EtAlii.xTechnology.Logging;
    using System;
    using System.Reactive.Linq;

    public class TweetImporter : ITweetImporter
    {
        private IDisposable _subscription;
        private readonly ILogger _logger;

        public TweetImporter(ILogger logger)
        {
            _logger = logger;
        }

        public void Start()
        {
            _logger.Info("Starting Twitter provider");

            _subscription = Observable.Interval(TimeSpan.FromSeconds(60))
                                      .Subscribe(Add);

            _logger.Info("Started Twitter provider");
        }

        public void Stop()
        {
            _logger.Info("Stopping Twitter provider");

            _subscription.Dispose();
            _subscription = null;

            _logger.Info("Stopped Twitter provider");
        }

        private void Add(long Time)
        {
            _logger.Info("Checking Twitter for new content");
        }
    }
}