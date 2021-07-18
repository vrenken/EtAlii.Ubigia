// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    /// <summary>
    /// Represents a single space in a <see cref="Storage"/>.
    /// </summary>
    public class Space : IIdentifiable
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <summary>
        /// The account to which this Space belongs.
        /// </summary>
        public Guid AccountId { get; set; }
    }
}
