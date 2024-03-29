﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Diagnostics;
// Summary:

// Remarks:

/// <summary>
/// Determines what the raw data for a given sampled metric is, and how it has
/// to be processed to produce final data point values for display.
/// </summary>
/// <remarks>
/// in many cases it is necessary to store raw facts that are translated into
/// the final display value during the display process so that they work regardless
/// of time resolution.
/// for example, to determine the percentage of processor time used for an activity,
/// you need to know a time interval to look across (say per second, per hour,
/// etc.), how many units of work were possible during that interval (time slices
/// of the processor) and how many were used by the process. by specfying the
/// totalfraction type, the metric display system will automatically inspect
/// the raw and baseline values then translate them into a percentage.
/// for more information on how to design sampled metrics including picking a
/// sampling type, see developer's reference - metrics - designing sampled metrics.
/// this enumeration is conceptually similar to the performance counter type
/// enumeration provided by the runtime, but has been simplified for easier use.
/// </remarks>
public enum SamplingType
{
    /// <summary>
    /// Each sample is the raw value for display as this data point.
    /// </summary>
    RawCount = 0,
    /// <summary>
    /// Each sample has the raw numerator and denominator of a fraction for display as the value for this data point.
    /// </summary>
    RawFraction = 1,
    /// <summary>
    /// Each sample is the incremental change in the value for display as this data point.
    /// </summary>
    IncrementalCount = 2,
    /// <summary>
    /// Each sample has the separate incremental changes to the numerator and denominator of the fraction for display as this data point.
    /// </summary>
    IncrementalFraction = 3,
    /// <summary>
    /// Each sample is the cumulative total of display value data points.
    /// </summary>
    TotalCount = 4,
    /// <summary>
    /// Each sample has the separate cumulative totals of the numerators and denominators of fraction value data points.
    /// </summary>
    TotalFraction = 5,
}
