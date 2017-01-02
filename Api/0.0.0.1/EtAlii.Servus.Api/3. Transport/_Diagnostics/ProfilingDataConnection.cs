namespace EtAlii.Servus.Api.Transport
{
    using System;
    using EtAlii.xTechnology.Logging;

    public class ProfilingDataConnection : IDataConnection
    {
        private const string _openingDurationCounter = "DataConnection.Opening.Duration";
        private const string _closingDurationCounter = "DataConnection.Closing.Duration";
        private const string _connectionCounter = "DataConnection.Connections";

        private readonly IDataConnection _connection;
        private readonly IProfiler _profiler;

        private static int _currentOpening;
        private static int _currentOpen;
        private static int _totalOpened;
        private static int _currentClosing;
        private static int _totalClosed;

        public Storage Storage { get { return _connection.Storage; } }
        public Account Account { get { return _connection.Account; } }
        public Space Space { get { return _connection.Space; } }

        public IRootContext Roots { get { return _connection.Roots; } }
        public IEntryContext Entries { get { return _connection.Entries; } }
        public IContentContext Content { get { return _connection.Content; } }
        public IPropertyContext Properties { get { return _connection.Properties; } }

        public IConnectionStatusProvider Status { get { return _connection.Status; } }

        internal ProfilingDataConnection(
            IDataConnection connection,
            IProfiler profiler)
        {
            _connection = connection;
            _profiler = profiler;

            profiler.Register(_openingDurationCounter, SamplingType.RawCount, "Milliseconds", "Opening a connection", "The time it takes for the Open method to execute");
            profiler.Register(_closingDurationCounter, SamplingType.RawCount, "Milliseconds", "Closing a connection", "The time it takes for the Close method to execute");


            var values = new EventMetricValue[]
            {
                new EventMetricValue { ValueName = "Current opening", ValueType = typeof(int), SummaryFunction = SummaryFunction.Count, UnitCaption = "Count", MetricCaption = "Current opening connections", Description = "The number of connections being opened at a specific moment"},
                new EventMetricValue { ValueName = "Total opened", ValueType = typeof(int), SummaryFunction = SummaryFunction.Count, UnitCaption = "Count", MetricCaption = "Total opened", Description = "The number of times the Open method has executed" },
                new EventMetricValue { ValueName = "Current open", ValueType = typeof(int), SummaryFunction = SummaryFunction.Count, UnitCaption = "Count", MetricCaption = "Current open connections", Description = "The number of open connections at a specific moment" },

                new EventMetricValue { ValueName = "Current closing", ValueType = typeof(int), SummaryFunction = SummaryFunction.Count, UnitCaption = "Count", MetricCaption = "Current closing connections", Description = "The number of connections being closed at a specific moment" },
                new EventMetricValue { ValueName = "Total closed", ValueType = typeof(int), SummaryFunction = SummaryFunction.Count, UnitCaption = "Count", MetricCaption = "Total closed connections", Description = "The number of times the Close method has executed" },
            };

            profiler.RegisterEventMetric(_connectionCounter, values);

            WriteEvent();
        }

        public void Open(string address, string accountName, string spaceName)
        {
            _currentOpening += 1;
            WriteEvent();
            
            var start = Environment.TickCount;
            _connection.Open(address, accountName, spaceName);
            _profiler.WriteSample(_openingDurationCounter, Environment.TickCount - start);

            _totalOpened += 1;
            _currentOpen += 1;
            _currentOpening -= 1;
            WriteEvent();
        }

        public void Open(string address, string accountName, string password, string spaceName)
        {
            _currentOpening += 1;
            WriteEvent();

            var start = Environment.TickCount;
            _connection.Open(address, accountName, password, spaceName);
            _profiler.WriteSample(_openingDurationCounter, Environment.TickCount - start);

            _totalOpened += 1;
            _currentOpen += 1;
            _currentOpening -= 1;
            WriteEvent();
        }

        public void Close()
        {
            _currentClosing += 1;
            WriteEvent();

            var start = Environment.TickCount;
            _connection.Close();
            _profiler.WriteSample(_closingDurationCounter, Environment.TickCount - start);

            _totalClosed += 1;
            _currentOpen -= 1;
            _currentClosing -= 1;
            WriteEvent();
        }

        private void WriteEvent()
        {
            _profiler.WriteEvent(_connectionCounter, _currentOpening, _totalOpened, _currentOpen, _currentClosing, _totalClosed);

        }
    }
}
