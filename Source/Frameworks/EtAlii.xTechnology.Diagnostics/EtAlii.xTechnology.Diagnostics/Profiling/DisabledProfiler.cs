// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Diagnostics
{
    using System;

    public class DisabledProfiler : IProfiler
    {
        public void Register(string counterName, SamplingType samplingType, string unitCaption, string metricCaption,
            string description)
        {
            // A disabled profiler has nothing to do (yet).
        }

        public void WriteSample(string counterName, double sampleData)
        {
            // A disabled profiler has nothing to do (yet).
        }

        public void RegisterEventMetric(
            string counterName, 
            string valueName, 
            Type valueType, 
            SummaryFunction summaryFunction, 
            string unitCaption, 
            string metricCaption,
            string description)
        {
            // A disabled profiler has nothing to do (yet).
        }

        public void RegisterEventMetric(string counterName, EventMetricValue[] values)
        {   
            // A disabled profiler has nothing to do (yet).
        }

        public void WriteEvent(string counterName, params object[] sampleDatas)
        {
            // A disabled profiler has nothing to do (yet).
        }
    }
}
