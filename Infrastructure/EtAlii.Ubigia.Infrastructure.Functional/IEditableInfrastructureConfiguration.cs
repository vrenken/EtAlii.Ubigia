namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public interface IEditableInfrastructureConfiguration
    {
        ILogicalContext Logical { get; set; }

        string Name { get; set; }

        Uri Address { get; set; }

        Func<Container, Func<Container, object>[], object>[] ComponentManagerFactories { get; set; }

        Func<Container, object>[] ComponentFactories { get; set; }

        ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; set; }

        Func<Container, IInfrastructure> GetInfrastructure { get; set; }

    }
}