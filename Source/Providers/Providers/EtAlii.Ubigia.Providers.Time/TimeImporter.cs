namespace EtAlii.Ubigia.Provisioning.Time
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using Serilog;

    public class TimeImporter : ITimeImporter
    {
        private IDisposable _subscription;
        private readonly ILogger _logger = Log.ForContext<ITimeImporter>();
        private readonly IProviderContext _context;

        //private readonly List<IReadOnlyEntry> _yearEntries
        //private readonly List<IReadOnlyEntry> _monthEntries
        //private readonly List<IReadOnlyEntry> _dayEntries
        //private readonly List<IReadOnlyEntry> _hourEntries
        //private readonly List<IReadOnlyEntry> _minuteEntries
        //private readonly List<IReadOnlyEntry> _secondEntries
        //private DateTime _lastTime
        private readonly bool addEachTenSeconds = false;

        public TimeImporter(IProviderContext context)
        {
            _context = context;

            //_yearEntries = new List<IReadOnlyEntry>()
            //_monthEntries = new List<IReadOnlyEntry>()
            //_dayEntries = new List<IReadOnlyEntry>()
            //_hourEntries = new List<IReadOnlyEntry>()
            //_minuteEntries = new List<IReadOnlyEntry>()
            //if [addEachTenSeconds]
            //[
            //    _secondEntries = new List<IReadOnlyEntry>()
            //]
        }

        public void Start()
        {
            _logger.Information("Starting time provider");

            Setup();

            _logger.Information("Started time provider");
        }

        public void Stop()
        {
            _logger.Information("Stopping time provider");

            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            _logger.Information("Stopped time provider");
        }

        private async Task Add(long time)
        {
            _logger.Information("Adding time to space");

            try
            {
                var utcNow = DateTime.UtcNow;
                string datePath;

                if(addEachTenSeconds)
                {
                    datePath = string.Format("/{0} += {1:yyyy}/{1:MM}/{1:dd}/{1:HH}/{1:mm}/{1:ss}", "Time", utcNow);
                }
                else
                {
                    datePath = string.Format("/{0} += {1:yyyy}/{1:MM}/{1:dd}/{1:HH}/{1:mm}", "Time", utcNow);
                }

                var sequenceResult = await _context.SystemScriptContext.Process(datePath);
                await sequenceResult.Output.LastOrDefaultAsync();

                //_lastTime = utcNow

            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to add time to space");
            }

        }

//        private string GetType(DateTime utcTime, string format)
//        [
//            return utcTime.ToString(format)
//        ]
        private void Setup()
        {
            if (addEachTenSeconds)
            {
                _subscription = Observable.Interval(TimeSpan.FromSeconds(10))
                                          .SubscribeAsync(Add);
            }
            else
            {
                _subscription = Observable.Interval(TimeSpan.FromMinutes(1))
                                          .SubscribeAsync(Add);
            }
        }
    }
}