// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest;

using EtAlii.xTechnology.Hosting;

public class UserRestServiceFactory : IServiceFactory
{
    public IService Create(ServiceConfiguration configuration) => new UserRestService(configuration);
}
