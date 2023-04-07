// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Threading.Tasks;

public class SystemStatusContext : ISystemStatusContext
{
    /// <inheritdoc />
    public SystemStatus Status { get; private set; }

    /// <inheritdoc />
    public DateTimeOffset StartTime { get; private set; }

    /// <inheritdoc />
    public TimeSpan Uptime => DateTimeOffset.Now - StartTime;

    /// <inheritdoc />
    public DateTimeOffset FirstStartTime { get; private set; }

    /// <inheritdoc />
    public TimeSpan TotalUptime { get; private set; }

    private readonly ISystemStatusChecker _systemStatusChecker;

    public SystemStatusContext(ISystemStatusChecker systemStatusChecker)
    {
        _systemStatusChecker = systemStatusChecker;
    }

    public async Task Update()
    {
        Status = await _systemStatusChecker.DetermineSystemStatus().ConfigureAwait(false);
    }
}
