// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Linq;

    public static class ExtensibleUseExtension
    {
        public static TExtensible Use<TExtensible>(this TExtensible extensible, IExtension[] extensions)
            where TExtensible: IExtensible
        {
            if (extensions == null)
            {
                throw new ArgumentException("No extensions specified", nameof(extensions));
            }

            extensible.Extensions = extensible.Extensions
                .Concat(extensions)
                .Distinct()
                .ToArray();
            return extensible;
        }
    }
}
