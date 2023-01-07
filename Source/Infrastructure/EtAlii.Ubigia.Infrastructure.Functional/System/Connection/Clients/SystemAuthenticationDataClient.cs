// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal partial class SystemAuthenticationDataClient : IAuthenticationDataClient
    {
        private readonly IFunctionalContext _functionalContext;

        public SystemAuthenticationDataClient(IFunctionalContext functionalContext)
        {
            _functionalContext = functionalContext;
        }

        public Task Connect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect()
        {
            return Task.CompletedTask;
        }
    }
}
