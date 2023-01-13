// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Threading.Tasks;

public interface ISystemStatusContext
{
    bool SetupIsNeeded { get; }
    bool SystemIsOperational { get; }
    DateTimeOffset StartTime { get; }
    DateTimeOffset FirstStartTime { get; }

    Task Update();
}
