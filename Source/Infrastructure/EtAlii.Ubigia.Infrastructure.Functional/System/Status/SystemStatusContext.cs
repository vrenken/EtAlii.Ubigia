// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Threading.Tasks;

public class SystemStatusContext : ISystemStatusContext
{
    private readonly ISystemStatusChecker _systemStatusChecker;
    public bool SetupIsNeeded { get; private set; }
    public bool SystemIsOperational { get; private set; }
    public DateTimeOffset StartTime { get; private set; }
    public TimeSpan Uptime => DateTimeOffset.Now - StartTime;
    public DateTimeOffset FirstStartTime { get; private set; }

    public SystemStatusContext(ISystemStatusChecker systemStatusChecker)
    {
        _systemStatusChecker = systemStatusChecker;
    }

    public async Task Update()
    {
        SetupIsNeeded = await _systemStatusChecker.DetermineIfSetupIsNeeded().ConfigureAwait(false);
        SystemIsOperational = await _systemStatusChecker.DetermineIfSystemIsOperational().ConfigureAwait(false);
    }
}
