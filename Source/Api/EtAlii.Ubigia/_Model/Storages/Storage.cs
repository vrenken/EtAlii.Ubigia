// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    /// <summary>
    /// A Ubigia storage, including it's address where it can be found.
    /// </summary>
    public sealed class Storage : IIdentifiable
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
