// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public sealed class AccountTemplate
    {
        /// <summary>
        /// A list of all account templates available.
        /// </summary>
        public static AccountTemplate[] All { get; }

        /// <summary>
        /// Returns the account template with which a user account needs to be instantiated.
        /// </summary>
        public static AccountTemplate User { get; }

        /// <summary>
        /// Returns the account template with which a system account needs to be instantiated.
        /// </summary>
        public static AccountTemplate System { get; }

        /// <summary>
        /// Returns the account template with which an administrator account needs to be instantiated.
        /// </summary>
        public static AccountTemplate Administrator { get; }


        /// <summary>
        /// The name of the account template.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The roles to be assigned to the account.
        /// </summary>
        public string[] RolesToAssign { get; }

        /// <summary>
        /// The spaces to create for the account.
        /// </summary>
        public SpaceTemplate[] SpacesToCreate { get; }

        static AccountTemplate()
        {
            User = new AccountTemplate
            (
                name: AccountName.User,
                rolesToAssign: new[] {Role.User,},
                spacesToCreate: new[] {SpaceTemplate.Configuration, SpaceTemplate.Data}
            );
            System = new AccountTemplate
            (
                name: AccountName.System,
                rolesToAssign: new [] { Role.System },
                spacesToCreate: new [] { SpaceTemplate.System, SpaceTemplate.Configuration, SpaceTemplate.Metrics }
            );
            Administrator = new AccountTemplate
            (
                name: AccountName.Administrator,
                rolesToAssign: new [] { Role.Admin, Role.User },
                spacesToCreate: new [] { SpaceTemplate.Configuration, SpaceTemplate.Data  }
            );

            All = new[]
            {
                User,
                System,
                Administrator
            };
        }

        private AccountTemplate(
            string name,
            string[] rolesToAssign,
            SpaceTemplate[] spacesToCreate)
        {
            Name = name;
            RolesToAssign = rolesToAssign;
            SpacesToCreate = spacesToCreate;
        }
    }
}
