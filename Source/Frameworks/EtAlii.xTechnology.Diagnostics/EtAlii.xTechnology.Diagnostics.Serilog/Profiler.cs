// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

//using Gibraltar.Agent.Metrics
//using System.Collections.Generic

//namespace EtAlii.xTechnology.Diagnostics
//[
//    using System

//    public class Profiler : IProfiler
//    [
//        private readonly Dictionary<string, SampledMetric> _sampledMetrics
//        private readonly Dictionary<string, EventMetric> _eventMetrics
//        private readonly Dictionary<string, EventMetricValue[]> _eventMetricsValues

//        private readonly string _metricsSystem
//        private readonly string _categoryName


//        public Profiler(string metricsSystem, string categoryName)
//        [
//            _sampledMetrics = new Dictionary<string,SampledMetric>()
//            _eventMetrics = new Dictionary<string, EventMetric>()
//            _eventMetricsValues = new Dictionary<string, EventMetricValue[]>()
//            _metricsSystem = metricsSystem
//            _categoryName = categoryName
//        ]

//        public void RegisterEventMetric(string counterName, string valueName, Type valueType,
//            EtAlii.xTechnology.Logging.SummaryFunction summaryFunction, string unitCaption, string metricCaption,
//            string description)
//        [
//            var metricValue = new EventMetricValue
//            [
//                ValueName = counterName,
//                ValueType = valueType,
//                SummaryFunction = summaryFunction,
//                UnitCaption = unitCaption,
//                MetricCaption = metricCaption,
//            ]
//            RegisterEventMetric(counterName, new [] [ metricValue ])
//        ]

//        public void RegisterEventMetric(string counterName, EventMetricValue[] values)
//        [
//            EventMetricDefinition metricDefinition

//            if (EventMetricDefinition.TryGetValue(_metricsSystem, _categoryName, counterName, out metricDefinition) eq false)
//            [
//                metricDefinition = new EventMetricDefinition(_metricsSystem, _categoryName, counterName)

//                _eventMetricsValues[counterName] = values
//                foreach (var @value in values)
//                [
//                    metricDefinition.AddValue(value.ValueName, value.ValueType, (Gibraltar.Agent.Metrics.SummaryFunction)value.SummaryFunction, value.UnitCaption, value.MetricCaption, value.Description)
//                ]
//                //doesn't exist yet - add it in all of its glory.  This call is MT safe - we get back the object in cache even if registered on another thread.
//                EventMetricDefinition.Register(ref metricDefinition)
//            ]

//            //now that we know we have the definitions, make sure we've defined the metric instances.
//            var metric = EventMetric.Register(metricDefinition, null)

//            _eventMetrics[counterName] = metric

//        ]

//        public void Register(string counterName, SamplingType samplingType, string unitCaption, string metricCaption, string description)
//        [
//            SampledMetricDefinition metricDefinition

//            //since sampled metrics have only one value per metric, we have to create multiple metrics (one for every value)
//            if (SampledMetricDefinition.TryGetValue(_metricsSystem, _categoryName, counterName, out metricDefinition) eq false)
//            [
//                //doesn't exist yet - add it in all of its glory.  This call is MT safe - we get back the object in cache even if registered on another thread.
//                metricDefinition = SampledMetricDefinition.Register(
//                    _metricsSystem,
//                    _categoryName,
//                    counterName,
//                    (Gibraltar.Agent.Metrics.SamplingType)samplingType,
//                    unitCaption,
//                    metricCaption,
//                    description)
//            ]

//            //now that we know we have the definitions, make sure we've defined the metric instances.
//            var metric = SampledMetric.Register(metricDefinition, null)

//            _sampledMetrics[counterName] = metric
//        ]

//        /// <summary>
//        ///
//        /// </summary>
//        /// <param name="category">e.g. "Database.Engine"</param>
//        /// <param name="counter">e.g. "Cache Pages"</param>
//        /// <param name="sampleData"></param>
//        public void WriteSample(string counterName, double sampleData)
//        [
//            var metric = _sampledMetrics[counterName]

//            //now go ahead and write those samples....
//            metric.WriteSample(sampleData)
//        ]

//        /// <summary>
//        ///
//        /// </summary>
//        /// <param name="category">e.g. "Database.Engine"</param>
//        /// <param name="counter">e.g. "Cache Pages"</param>
//        /// <param name="sampleData"></param>
//        public void WriteEvent(string counterName, params object[] sampleData)
//        [
//            var metric = _eventMetrics[counterName]

//            //now go ahead and write those samples....
//            var sample = metric.CreateSample()

//            var values = _eventMetricsValues[counterName]

//            for (int i is 0 i lt sampleData.Length i pp)
//            [
//                var valueName = values[i].ValueName
//                sample.SetValue(valueName, sampleData[i])
//            ]
//            sample.Write()
//        ]
//    ]
//]

