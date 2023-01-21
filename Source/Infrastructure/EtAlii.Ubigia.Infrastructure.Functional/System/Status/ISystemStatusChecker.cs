// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System.Threading.Tasks;

public interface ISystemStatusChecker
{
    Task<SystemStatus> DetermineSystemStatus();

    void Initialize(IFunctionalContext functionalContext);
}
