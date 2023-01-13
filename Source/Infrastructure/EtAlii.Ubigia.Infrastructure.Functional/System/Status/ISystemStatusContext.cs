// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Threading.Tasks;

public interface ISystemStatusContext
{
    /// <summary>
    /// Returns true if some prerequisite is missing and the system really needs to be setup accordingly.
    /// </summary>
    bool SetupIsNeeded { get; }

    /// <summary>
    /// Returns true if the system can function. That is, setup or maintenance can still be needed,
    /// but the system nevertheless is able to deliver content through its APIs.
    /// </summary>
    bool SystemIsOperational { get; }

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
