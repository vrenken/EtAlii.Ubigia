// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    /// <summary>
    /// The role that a user can have in Ubigia.
    /// </summary>
    public static class Role
    {
        /// <summary>
        /// The user account is needed for system specific-purposes.
        /// </summary>
        public const string System = "System";

        /// <summary>
        /// The user account is an administrator.
        /// </summary>
        public const string Admin = "Admin";

        /// <summary>
        /// The user account is a real user.
        /// </summary>
        public const string User = "User";
    }
}
