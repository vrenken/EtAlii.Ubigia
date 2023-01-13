// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc;

using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.xTechnology.MicroContainer;

public class UserApiScaffolding : IScaffolding
{
    private readonly IFunctionalContext _functionalContext;

    public UserApiScaffolding(IFunctionalContext functionalContext)
    {
        _functionalContext = functionalContext;
    }

    /// <inheritdoc />
    public void Register(IRegisterOnlyContainer container)
    {
        container.Register(() => _functionalContext);
        container.Register(() => _functionalContext.Storages);
        container.Register(() => _functionalContext.Accounts);
        container.Register(() => _functionalContext.Spaces);
        container.Register(() => _functionalContext.Roots);
        container.Register(() => _functionalContext.Entries);
        container.Register(() => _functionalContext.Properties);
        container.Register(() => _functionalContext.Content);
        container.Register(() => _functionalContext.ContentDefinition);
        container.Register(() => _functionalContext.ContextCorrelator);
    }
}
