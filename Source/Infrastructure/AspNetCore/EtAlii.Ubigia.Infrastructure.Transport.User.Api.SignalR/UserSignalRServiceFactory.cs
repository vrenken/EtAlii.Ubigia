// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
    using EtAlii.xTechnology.Hosting;

    public class UserSignalRServiceFactory : IServiceFactory
    {
        public IService Create(ServiceConfiguration configuration) => new UserSignalRService(configuration);
    }
}
