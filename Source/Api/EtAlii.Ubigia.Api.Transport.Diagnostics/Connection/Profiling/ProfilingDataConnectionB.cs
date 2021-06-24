// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

//namespace EtAlii.Ubigia.Api.Transport.Diagnostics
//[
//    using System
//    using System.Threading.Tasks
//    using EtAlii.Ubigia.Api.Transport
//    using EtAlii.xTechnology.Logging

//    public class ProfilingDataConnection : IDataConnection
//    [
//        private const string _openingDurationCounter = "DataConnection.Opening.Duration"
//        private const string _closingDurationCounter = "DataConnection.Closing.Duration"
//        private const string _connectionCounter = "DataConnection.Connections"

//        private readonly IDataConnection _decoree
//        private readonly IProfiler _profiler

//        private static int _currentOpening
//        private static int _currentOpen
//        private static int _totalOpened
//        private static int _currentClosing
//        private static int _totalClosed

//        public Storage Storage => _decoree.Storage
//        public Account Account => _decoree.Account
//        public Space Space => _decoree.Space

//        public IRootContext Roots => _decoree.Roots
//        public IEntryContext Entries => _decoree.Entries
//        public IContentContext Content => _decoree.Content
//        public IPropertiesContext Properties => _decoree.Properties

//        public bool IsConnected => _decoree.IsConnected
//        public IDataConnectionConfiguration Configuration => _decoree.Configuration

//        internal ProfilingDataConnection(
//            IDataConnection decoree,
//            IProfiler profiler)
//        [
//            _decoree = decoree
//            _profiler = profiler

//            profiler.Register(_openingDurationCounter, SamplingType.RawCount, "Milliseconds", "Opening a connection", "The time it takes for the Open method to execute")
//            profiler.Register(_closingDurationCounter, SamplingType.RawCount, "Milliseconds", "Closing a connection", "The time it takes for the Close method to execute")


//            var values = new[]
//            [
//                new EventMetricValue [ ValueName = "Current opening", ValueType = typeof(int), SummaryFunction = SummaryFunction.Count, UnitCaption = "Count", MetricCaption = "Current opening connections", Description = "The number of connections being opened at a specific moment"],
//                new EventMetricValue [ ValueName = "Total opened", ValueType = typeof(int), SummaryFunction = SummaryFunction.Count, UnitCaption = "Count", MetricCaption = "Total opened", Description = "The number of times the Open method has executed" ],
//                new EventMetricValue [ ValueName = "Current open", ValueType = typeof(int), SummaryFunction = SummaryFunction.Count, UnitCaption = "Count", MetricCaption = "Current open connections", Description = "The number of open connections at a specific moment" ],

//                new EventMetricValue [ ValueName = "Current closing", ValueType = typeof(int), SummaryFunction = SummaryFunction.Count, UnitCaption = "Count", MetricCaption = "Current closing connections", Description = "The number of connections being closed at a specific moment" ],
//                new EventMetricValue [ ValueName = "Total closed", ValueType = typeof(int), SummaryFunction = SummaryFunction.Count, UnitCaption = "Count", MetricCaption = "Total closed connections", Description = "The number of times the Close method has executed" ],
//            ]
//            profiler.RegisterEventMetric(_connectionCounter, values)

//            WriteEvent()
//        ]
//        public async Task Open()
//        [
//            _currentOpening += 1
//            WriteEvent()

//            var start = Environment.TickCount
//            await _decoree.Open()
//            _profiler.WriteSample(_openingDurationCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds)

//            _totalOpened += 1
//            _currentOpen += 1
//            _currentOpening -= 1
//            WriteEvent()
//        ]
//        public async Task Close()
//        [
//            _currentClosing += 1
//            WriteEvent()

//            var start = Environment.TickCount
//            await _decoree.Close()
//            _profiler.WriteSample(_closingDurationCounter, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds)

//            _totalClosed += 1
//            _currentOpen -= 1
//            _currentClosing -= 1
//            WriteEvent()
//        ]
//        private void WriteEvent()
//        [
//            _profiler.WriteEvent(_connectionCounter, _currentOpening, _totalOpened, _currentOpen, _currentClosing, _totalClosed)

//        ]
//    ]
//]
