// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer
{
    using System.Linq;

    /// <summary>
    /// Use this interface to extend configurations, options etc.
    /// Don't use this outside .Use methods as else the user could get confused - The fluent builder pattern
    /// should be adhered to by all configuration variations.
    /// </summary>
    public interface IExtensible
    {
        /// <summary>
        /// Gets or sets the extensions assigned to the extensible instance.
        /// </summary>
        IExtension[] Extensions { get; set; }

        /// <summary>
        /// Returns the extensions of the type specified by TExtension.
        /// </summary>
        /// <typeparam name="TExtension"></typeparam>
        /// <returns></returns>
        TExtension[] GetExtensions<TExtension>()
            where TExtension : IExtension
        {
            return Extensions.OfType<TExtension>().ToArray();
        }
    }
}
