// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal class SystemPropertiesDataClient : SystemSpaceClientBase, IPropertiesDataClient
    {
        private readonly IFunctionalContext _functionalContext;

        public SystemPropertiesDataClient(IFunctionalContext functionalContext)
        {
            _functionalContext = functionalContext;
        }

        public Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            _functionalContext.Properties.Store(identifier, properties);
            return Task.CompletedTask;
        }

        public Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope)
        {
            var result = _functionalContext.Properties.Get(identifier);
            return Task.FromResult(result);
        }
    }
}
