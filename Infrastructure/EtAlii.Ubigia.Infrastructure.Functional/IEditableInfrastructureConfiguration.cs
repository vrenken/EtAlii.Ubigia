namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public interface IEditableInfrastructureConfiguration
    {
        ILogicalContext Logical { get; set; }

        string Name { get; set; }

        /// <summary>
        /// Editable access to the address of the management API.
        /// </summary>
        Uri ManagementAddress { get; set; }
        /// <summary>
        /// Editable access to the address of the data API.
        /// </summary>
        Uri DataAddress { get; set; }


        ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; set; }

        Func<Container, IInfrastructure> GetInfrastructure { get; set; }

    }
}