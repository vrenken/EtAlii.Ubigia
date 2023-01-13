// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using EtAlii.xTechnology.MicroContainer;

public class SystemConnectionCreationProxy : ISystemConnectionCreationProxy
{
    private IFunctionalContext _functionalContext;

    public ISystemConnection Request()
    {
        // This Options.ConfigurationRoot refers to the host configuration root.
        // In order to use it for the system connection it should have the entries needed by the API subsystems.
        var systemConnectionOptions = new SystemConnectionOptions(_functionalContext.Options.ConfigurationRoot)
            .Use(_functionalContext);
        return Factory.Create<ISystemConnection>(systemConnectionOptions);
    }

    void ISystemConnectionCreationProxy.Initialize(IFunctionalContext functionalContext)
    {
        _functionalContext = functionalContext;
    }
}
