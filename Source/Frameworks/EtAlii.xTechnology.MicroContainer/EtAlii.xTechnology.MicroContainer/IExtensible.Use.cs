// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Linq;

    public static class ExtensibleUseExtension
    {
        public static TExtensible Use<TExtensible, TExtension>(this TExtensible extensible, TExtension[] extensions)
            where TExtensible: IExtensible
            where TExtension : IExtension
        {
            if (extensions == null)
            {
                throw new ArgumentException("No extensions specified", nameof(extensions));
            }

            extensible.Extensions = extensible.Extensions
                .Concat(extensions.Cast<IExtension>()) // TODO: This cast feels not needed.
                .Distinct()
                .ToArray();
            return extensible;
        }
    }
}
