// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Threading.Tasks;

public interface ISystemStatusContext
{
    /// <summary>
    /// Returns the status of the whole system.
    /// </summary>
    SystemStatus Status { get; }

    /// <summary>
    /// The last time the system has been started.
    /// </summary>
    DateTimeOffset StartTime { get; }

    /// <summary>
    /// The first time the system has been started.
    /// </summary>
    DateTimeOffset FirstStartTime { get; }

    /// <summary>
    /// The time the system currently has been running.
    /// </summary>
    TimeSpan Uptime { get; }

    /// <summary>
    /// The time the system has been running in total.
    /// </summary>
    TimeSpan TotalUptime { get; }

    Task Update();
}
