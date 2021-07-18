// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    /// <summary>
    /// A simple POCO object that represents a user in the Ubigia systems.
    /// </summary>
    public class Account : IIdentifiable
    {
        /// <summary>
        /// Create a new account instance.
        /// </summary>
        public Account()
        {
            Roles = Array.Empty<string>();
        }

        /// <summary>
        /// The unique Guid with which the user can be represented in the Ubigia systems.
        /// </summary>
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
