// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    /// <summary>
    /// Use this interface to access properties of a configuration.
    /// Don't use this outside .Use methods as else the user could get confused - The fluent builder pattern
    /// should be adhered to by all configuration variations. 
    /// </summary>
    public interface IEditableConfiguration
    {
        /// <summary>
        /// Gets or sets the extensions in the Configuration.
        /// </summary>
        IExtension[] Extensions { get; set; }
    }
}