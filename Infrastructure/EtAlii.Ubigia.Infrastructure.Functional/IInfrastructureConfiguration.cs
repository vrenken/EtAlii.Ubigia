namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Logical;

    public interface IInfrastructureConfiguration : IConfiguration
    {
        ILogicalContext Logical { get; }

        /// <summary>
        /// The address of the management API.
        /// </summary>
        Uri ManagementAddress { get; }

        /// <summary>
        /// The address of the data API.
        /// </summary>
        Uri DataAddress { get; }

        string Name { get; }

        ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; }
    }
}