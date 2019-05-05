namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public interface IInfrastructureConfiguration : IConfiguration<InfrastructureConfiguration>
    {
        ILogicalContext Logical { get; }

        Uri Address { get; }

        string Name { get; }

        ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; }
        
        // TODO: These two properties should be typed.
        Func<Container, Func<Container, object>[], object>[] ComponentManagerFactories { get; }
        Func<Container, object>[] ComponentFactories { get; }
    }
}