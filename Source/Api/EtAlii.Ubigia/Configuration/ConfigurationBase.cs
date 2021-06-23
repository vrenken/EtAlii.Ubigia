// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Linq;

    /// <summary>
    /// This is the base class for all configuration classes.
    /// It provides out of the box support for extensions.
    /// </summary>
    public abstract class ConfigurationBase : IConfiguration, IEditableConfiguration
    {
        /// <summary>
        /// The extensions added to this configuration.
        /// </summary>
        protected IExtension[] Extensions { get; private set; }
        
        /// <inheritdoc/>        
        IExtension[] IEditableConfiguration.Extensions { get => Extensions; set => Extensions = value; } 

        /// <summary>
        /// Creates a new Configuration instance. 
        /// </summary>
        protected ConfigurationBase()
        {
            Extensions = Array.Empty<IExtension>();
        }

        /// <inheritdoc/>
        public TExtension[] GetExtensions<TExtension>()
            where TExtension : IExtension
        {
            return Extensions.OfType<TExtension>().ToArray();
        }
    }
}