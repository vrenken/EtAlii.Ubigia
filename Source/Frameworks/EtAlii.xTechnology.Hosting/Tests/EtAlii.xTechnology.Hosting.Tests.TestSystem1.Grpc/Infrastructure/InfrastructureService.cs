// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
{
    using Microsoft.Extensions.Configuration;

    public class InfrastructureService : ServiceBase
    {
        public InfrastructureService(IConfigurationSection configuration)
            : base(configuration)
        {
        }
    }
}
