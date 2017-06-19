namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public class AccountTemplate
    {
        public static AccountTemplate[] All => _all.Value;

        private static readonly Lazy<AccountTemplate[]> _all = new Lazy<AccountTemplate[]>(() => new [] {
            User,
            System,
            Administrator
        });

        public static AccountTemplate User { get; } = new AccountTemplate
        (
            name: AccountName.User, 
            rolesToAssign: new [] { Role.User, },
            spacesToCreate: new []{ SpaceTemplate.Configuration, SpaceTemplate.Data }
        );

        public static AccountTemplate System { get; } = new AccountTemplate
        (
            name: AccountName.System,
            rolesToAssign: new [] { Role.System },
            spacesToCreate: new [] { SpaceTemplate.System, SpaceTemplate.Metrics }
        );

        public static AccountTemplate Administrator { get; } = new AccountTemplate
        (
            name: AccountName.Administrator,
            rolesToAssign: new [] { Role.Admin, Role.User },
            spacesToCreate: new [] { SpaceTemplate.Configuration, SpaceTemplate.Data  }
        );


        public string Name { get; }

        public string[] RolesToAssign { get; }

        public SpaceTemplate[] SpacesToCreate { get; }

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