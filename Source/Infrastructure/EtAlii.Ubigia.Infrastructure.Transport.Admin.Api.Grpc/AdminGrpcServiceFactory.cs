// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc;

using EtAlii.xTechnology.Hosting;

public class AdminGrpcServiceFactory : IServiceFactory
{
    public IService Create(ServiceConfiguration configuration) => new AdminGrpcService(configuration);
}
