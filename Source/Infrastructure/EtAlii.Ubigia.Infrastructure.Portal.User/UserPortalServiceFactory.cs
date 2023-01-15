// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Portal.User;

using EtAlii.xTechnology.Hosting;

public class UserPortalServiceFactory : IServiceFactory
{
    public IService Create(ServiceConfiguration configuration) => new UserPortalService(configuration);
}
