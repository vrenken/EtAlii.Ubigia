// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    /// <summary>
    /// A simple POCO object that represents a user in the Ubigia systems.
    /// </summary>
    public sealed class Account : IIdentifiable
    {
        /// <summary>
        /// Create a new account instance.
        /// </summary>
        public Account()
        {
            Roles = Array.Empty<string>();
        }

        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
