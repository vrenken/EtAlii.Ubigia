// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class AccountTemplate
    {
        public static AccountTemplate[] All { get; }

        public static AccountTemplate User { get; }

        public static AccountTemplate System { get; }

        public static AccountTemplate Administrator { get; }


        public string Name { get; }

        public string[] RolesToAssign { get; }

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
                spacesToCreate: new [] { SpaceTemplate.System, SpaceTemplate.Metrics }
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