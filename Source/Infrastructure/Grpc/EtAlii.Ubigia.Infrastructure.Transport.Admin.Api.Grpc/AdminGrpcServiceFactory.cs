// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using EtAlii.xTechnology.Hosting;

    public class AdminGrpcServiceFactory : INewServiceFactory
    {
        public INewService Create(ServiceConfiguration configuration)
        {
            return new AdminGrpcService(configuration);
        }
    }
}
