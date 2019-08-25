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


        ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; set; }

        Func<Container, IInfrastructure> GetInfrastructure { get; set; }

    }
}