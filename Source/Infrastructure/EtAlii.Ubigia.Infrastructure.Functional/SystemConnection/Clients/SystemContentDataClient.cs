// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.Ubigia.Api.Transport;

    internal partial class SystemContentDataClient : SystemSpaceClientBase, IContentDataClient
    {
        private readonly IFunctionalContext _functionalContext;

        public SystemContentDataClient(IFunctionalContext functionalContext)
        {
            _functionalContext = functionalContext;
        }
    }
}
