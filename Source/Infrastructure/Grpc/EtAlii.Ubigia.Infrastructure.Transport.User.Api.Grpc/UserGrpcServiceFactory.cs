// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc;

using EtAlii.xTechnology.Hosting;

public class UserGrpcServiceFactory : IServiceFactory
{
    public IService Create(ServiceConfiguration configuration) => new UserGrpcService(configuration);
}
