namespace EtAlii.xTechnology.Diagnostics
{
    using System;

    public class DisabledProfiler : IProfiler
    {
        public void Register(string counterName, SamplingType samplingType, string unitCaption, string metricCaption,
            string description)
        {
        }

        public void WriteSample(string counterName, double sampleData)
        {
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
        }

        public void RegisterEventMetric(string counterName, EventMetricValue[] values)
        {   
        }

        public void WriteEvent(string counterName, params object[] sampleData)
        {
        }
    }
}
