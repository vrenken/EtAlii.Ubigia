// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    /// <summary>
    /// Represents a single space in a <see cref="Storage"/>.
    /// </summary>
    public class Space : IIdentifiable
    {
        /// <summary>
        /// The unique identifier of the Space.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the Space.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The account to which this Space belongs.
        /// </summary>
        public Guid AccountId { get; set; }
    }
}
