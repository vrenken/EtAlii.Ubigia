// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public interface IEditableInfrastructureOptions
    {
        /// <summary>
        /// Editable access to the context that provides access to the logical layer of the codebase.
        /// </summary>
        ILogicalContext Logical { get; set; }

        /// <summary>
        /// Editable access to the name of the infrastructure.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Editable access to provide the details for all of the services provided by the hosted infrastructure.
        /// </summary>
        ServiceDetails[] ServiceDetails { get; set; }

        /// <summary>
        /// Editable access to a proxy wrapping system connection creation mechanisms.
        /// </summary>
        ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; set; }

        Action<IRegisterOnlyContainer> RegisterInfrastructureService { get; set; }
    }
}
