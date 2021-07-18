// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    /// <summary>
    /// Represents a root in a <see cref="Space"/>.
    /// Roots are used to start entity traversals from.
    /// </summary>
    public class Root : IIdentifiable
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <summary>
        /// The current (=last) Ubigia identifier from which traversal can commence.
        /// </summary>
        public Identifier Identifier { get; set; }
    }
}
