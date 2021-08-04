// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    public class ParserOptions : IExtensible
    {
        /// <summary>
        /// The extensions added to this configuration.
        /// </summary>
        public IExtension[] Extensions { get; private set; } = Array.Empty<IExtension>();

        /// <inheritdoc />
        IExtension[] IExtensible.Extensions { get => Extensions; set => Extensions = value; }

        /// <inheritdoc />
        TExtension[] IExtensible.GetExtensions<TExtension>() => Extensions.OfType<TExtension>().ToArray();
    }
}
