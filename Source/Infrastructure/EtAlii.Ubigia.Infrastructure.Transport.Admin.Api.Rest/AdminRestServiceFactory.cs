// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest;

using EtAlii.xTechnology.Hosting;

public class AdminRestServiceFactory : IServiceFactory
{
    public IService Create(ServiceConfiguration configuration) => new AdminRestService(configuration);
}
