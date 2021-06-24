// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using EtAlii.xTechnology.MicroContainer;

    /// <summary>
    /// Use this interface to define an extension that can be used by configuration/factory subsystem implementations.
    /// </summary>
    public interface IExtension
    {
        /// <summary>
        /// Initialize the Extension by adding the corresponding registrations to the specified container.
        /// </summary>
        /// <param name="container"></param>
        void Initialize(Container container);
    }
}
