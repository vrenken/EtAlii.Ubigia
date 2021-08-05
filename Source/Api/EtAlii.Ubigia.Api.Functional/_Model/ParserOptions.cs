// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Configuration;

    public class ParserOptions : IExtensible
    {
        /// <summary>
        /// The client configuration root that will be used to configure the parser.
        /// </summary>
        public IConfiguration ConfigurationRoot { get; }

        /// <summary>
        /// The extensions added to this configuration.
        /// </summary>
        public IExtension[] Extensions { get; private set; } = Array.Empty<IExtension>();

        /// <inheritdoc />
        IExtension[] IExtensible.Extensions { get => Extensions; set => Extensions = value; }

        /// <inheritdoc />
        TExtension[] IExtensible.GetExtensions<TExtension>() => Extensions.OfType<TExtension>().ToArray();

        public ParserOptions(IConfiguration configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
        }
    }
}
