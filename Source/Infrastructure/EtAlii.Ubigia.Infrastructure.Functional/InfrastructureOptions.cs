// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class InfrastructureOptions : ConfigurationBase, IInfrastructureOptions, IEditableInfrastructureOptions
    {
        /// <inheritdoc />
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <inheritdoc />
        ILogicalContext IEditableInfrastructureOptions.Logical { get => Logical; set => Logical = value; }

        /// <inheritdoc />
        public ILogicalContext Logical { get; private set; }

        /// <inheritdoc />
        string IEditableInfrastructureOptions.Name { get => Name; set => Name = value; }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
        ServiceDetails[] IEditableInfrastructureOptions.ServiceDetails { get => ServiceDetails; set => ServiceDetails = value; }

        /// <inheritdoc />
        public ServiceDetails[] ServiceDetails { get; private set; } = Array.Empty<ServiceDetails>();

        /// <inheritdoc />
        ISystemConnectionCreationProxy IEditableInfrastructureOptions.SystemConnectionCreationProxy { get => SystemConnectionCreationProxy; set => SystemConnectionCreationProxy = value; }

        /// <inheritdoc />
        public ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; private set; }

        /// <inheritdoc />
        Func<Container, IInfrastructure> IEditableInfrastructureOptions.GetInfrastructure { get; set; }

        public InfrastructureOptions(IConfigurationRoot configurationRoot, ISystemConnectionCreationProxy systemConnectionCreationProxy)
        {
            ConfigurationRoot = configurationRoot;
            SystemConnectionCreationProxy = systemConnectionCreationProxy;
        }

        public IInfrastructure GetInfrastructure(Container container)
        {
            return ((IEditableInfrastructureOptions)this).GetInfrastructure(container);
        }
    }
}
