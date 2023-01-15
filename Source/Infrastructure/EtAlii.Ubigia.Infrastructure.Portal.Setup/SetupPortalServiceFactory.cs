// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Portal.Setup;

using EtAlii.xTechnology.Hosting;

public class SetupPortalServiceFactory : IServiceFactory
{
    public IService Create(ServiceConfiguration configuration) => new SetupPortalService(configuration);
}
