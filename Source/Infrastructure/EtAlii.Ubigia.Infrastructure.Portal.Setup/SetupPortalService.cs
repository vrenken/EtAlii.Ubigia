// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Portal.Setup;

using System;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.xTechnology.Hosting;

public class SetupPortalService : PortalServiceBase<SetupPortalService>
{
    public SetupPortalService(ServiceConfiguration configuration) : base(configuration)
    {
    }

    protected override bool ShouldActivate(IFunctionalContext functionalContext)
    {
        // When setup is needed we activate the setup portal.
        return functionalContext.Status.Status switch
        {
            SystemStatus.SetupIsNeeded => true,
            SystemStatus.SystemIsNonOperational => false,
            SystemStatus.SystemIsOperational => false,
            _ => throw new InvalidOperationException("Invalid SystemStatus provided")
        };
    }
}
