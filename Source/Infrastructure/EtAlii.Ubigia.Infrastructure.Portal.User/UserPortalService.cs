// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Portal.User;

using System;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.xTechnology.Hosting;

public class UserPortalService : PortalServiceBase<UserPortalService>
{
    public UserPortalService(ServiceConfiguration configuration) : base(configuration)
    {
    }

    protected override bool ShouldActivate(IFunctionalContext functionalContext)
    {
        return functionalContext.Status.Status switch
        {
            SystemStatus.SetupIsNeeded => false,
            SystemStatus.SystemIsNonOperational => false,
            SystemStatus.SystemIsOperational => true,
            _ => throw new InvalidOperationException("Invalid SystemStatus provided")
        };
    }
}
