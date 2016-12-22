
namespace EtAlii.xTechnology.Logging
{
    using System;

    /// <summary>
    /// Indicates the default way to interpret multiple values for display purposes
    /// 
    /// </summary>
    public class EventMetricValue
    {
        public string ValueName { get; set; }
        public Type ValueType { get; set; }
        public EtAlii.xTechnology.Logging.SummaryFunction SummaryFunction { get; set; }
        public string UnitCaption { get; set; }
        public string MetricCaption { get; set; }
        public string Description { get; set; }
    }
}
