namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public class InfrastructureConfiguration : Configuration, IInfrastructureConfiguration, IEditableInfrastructureConfiguration
    {
        /// <inheritdoc />
        ILogicalContext IEditableInfrastructureConfiguration.Logical { get => Logical; set => Logical = value; }

        /// <inheritdoc />
        public ILogicalContext Logical { get; private set; }

        /// <inheritdoc />
        string IEditableInfrastructureConfiguration.Name { get => Name; set => Name = value; }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
        ServiceDetails[] IEditableInfrastructureConfiguration.ServiceDetails { get => ServiceDetails; set => ServiceDetails = value; }
        
        /// <inheritdoc />
        public ServiceDetails[] ServiceDetails { get; private set; } = Array.Empty<ServiceDetails>();

        /// <inheritdoc />
        ISystemConnectionCreationProxy IEditableInfrastructureConfiguration.SystemConnectionCreationProxy { get => SystemConnectionCreationProxy; set => SystemConnectionCreationProxy = value; }
        /// <inheritdoc />
        public ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; private set; }

        Func<Container, IInfrastructure> IEditableInfrastructureConfiguration.GetInfrastructure { get; set; }

        public InfrastructureConfiguration(ISystemConnectionCreationProxy systemConnectionCreationProxy)
        {
            SystemConnectionCreationProxy = systemConnectionCreationProxy;
        }

        public IInfrastructure GetInfrastructure(Container container)
        {
            return ((IEditableInfrastructureConfiguration)this).GetInfrastructure(container);
        }
    }
}
