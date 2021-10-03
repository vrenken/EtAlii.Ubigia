// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
{
    using Microsoft.Extensions.DependencyInjection;

    public class StorageService : IService
    {
        /// <inheritdoc />
        public Status Status { get; }

        /// <inheritdoc />
        public ServiceConfiguration Configuration { get; }


        public StorageService(ServiceConfiguration configuration, Status status)
        {
            Configuration = configuration;
            Status = status;
        }

        public void ConfigureServices(IServiceCollection services) { }
    }
}
