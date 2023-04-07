// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Portal.Admin;

using System;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.xTechnology.Hosting;

public class AdminPortalService : PortalServiceBase<AdminPortalService>
{
    public AdminPortalService(ServiceConfiguration configuration) : base(configuration)
    {
    }

    protected override bool ShouldActivate(IFunctionalContext functionalContext)
    {
        return functionalContext.Status.Status switch
        {
            SystemStatus.SetupIsNeeded => false,
            SystemStatus.SystemIsNonOperational => true,
            SystemStatus.SystemIsOperational => true,
            _ => throw new InvalidOperationException("Invalid SystemStatus provided")
        };
    }
}
