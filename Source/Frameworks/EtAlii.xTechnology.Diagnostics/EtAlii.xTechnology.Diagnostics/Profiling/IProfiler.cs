// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Diagnostics;

using System;

public interface IProfiler
{
    void Register(string counterName, SamplingType samplingType, string unitCaption, string metricCaption, string description);

    void WriteSample(string counterName, double sampleData);

    void RegisterEventMetric(string counterName, EventMetricValue[] values);
    void RegisterEventMetric(string counterName, string valueName, Type valueType, SummaryFunction summaryFunction, string unitCaption, string metricCaption, string description);

    void WriteEvent(string counterName, params object[] sampleDatas);
}
