// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
{
    using Microsoft.Extensions.DependencyInjection;

    public class StorageService : IService
    {
        /// <inheritdoc />
        public ServiceConfiguration Configuration { get; }


        public StorageService(ServiceConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // For testing we don't have anything related to the services to configure.
        }
    }
}
