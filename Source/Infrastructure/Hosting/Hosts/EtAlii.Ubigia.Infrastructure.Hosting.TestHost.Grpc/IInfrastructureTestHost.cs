// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
{
    using EtAlii.Ubigia.Persistence;
    
    public interface IInfrastructureTestHost : IInfrastructureTestHostBase
    {
        IStorage Storage { get; }
    }
}