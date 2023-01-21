// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Portal.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Functional;

public class TestSystemStatusChecker : ISystemStatusChecker
{
    public required SystemStatus SystemStatus { get; init; }

    public Task<SystemStatus> DetermineSystemStatus() => Task.FromResult(SystemStatus);

    void ISystemStatusChecker.Initialize(IFunctionalContext functionalContext) { }
}
