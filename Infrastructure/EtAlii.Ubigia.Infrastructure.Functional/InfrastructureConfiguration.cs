namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public class InfrastructureConfiguration : Configuration, IInfrastructureConfiguration, IEditableInfrastructureConfiguration
    {
        ILogicalContext IEditableInfrastructureConfiguration.Logical { get => Logical; set => Logical = value; }
        public ILogicalContext Logical { get; private set; }

        string IEditableInfrastructureConfiguration.Name { get => Name; set => Name = value; }
        public string Name { get; private set; }

        /// <inheritdoc />
        Uri IEditableInfrastructureConfiguration.ManagementAddress { get => ManagementAddress; set => ManagementAddress = value; }
        /// <inheritdoc />
        public Uri ManagementAddress { get; private set; }

        /// <inheritdoc />
        Uri IEditableInfrastructureConfiguration.DataAddress { get => DataAddress; set => DataAddress = value; }
        /// <inheritdoc />
        public Uri DataAddress { get; private set; }

        ISystemConnectionCreationProxy IEditableInfrastructureConfiguration.SystemConnectionCreationProxy { get => SystemConnectionCreationProxy; set => SystemConnectionCreationProxy = value; }
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
