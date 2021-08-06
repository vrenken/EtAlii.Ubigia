﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.Ubigia.Infrastructure.Logical;
    using Microsoft.Extensions.Configuration;

    public interface IInfrastructureOptions : IExtensible
    {
        /// <summary>
        /// The host configuration root instance for the current application.
        /// </summary>
        /// <remarks>
        /// This is not the same configuration root as used by client API subsystems.
        /// </remarks>
        IConfigurationRoot ConfigurationRoot { get; }

        /// <summary>
        /// The context that provides access to the logical layer of the codebase.
        /// </summary>
        ILogicalContext Logical { get; }

        /// <summary>
        /// Returns the details for all of the services provided by the hosted infrastructure.
        /// </summary>
        ServiceDetails[] ServiceDetails { get; }

        /// <summary>
        /// The name of the infrastructure.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A proxy wrapping system connection creation mechanisms.
        /// </summary>
        ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; }
    }
}
