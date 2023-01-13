// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Diagnostics;

using System;

/// <summary>
/// Indicates the default way to interpret multiple values for display purposes
///
/// </summary>
public class EventMetricValue
{
    public string ValueName { get; set; }
    public Type ValueType { get; set; }
    public SummaryFunction SummaryFunction { get; set; }
    public string UnitCaption { get; set; }
    public string MetricCaption { get; set; }
    public string Description { get; set; }
}
