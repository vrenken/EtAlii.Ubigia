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

        public static AccountTemplate User => _user;

        private static readonly AccountTemplate _user = new AccountTemplate
        (
            name: AccountName.User, 
            rolesToAssign: new [] { Role.User, },
            spacesToCreate: new []{ SpaceTemplate.Configuration, SpaceTemplate.Data }
        );

        public static AccountTemplate System => _system;

        private static readonly AccountTemplate _system = new AccountTemplate
        (
            name: AccountName.System,
            rolesToAssign: new [] { Role.System },
            spacesToCreate: new [] { SpaceTemplate.System, SpaceTemplate.Metrics }
        );

        public static AccountTemplate Administrator => _administrator;

        private static readonly AccountTemplate _administrator = new AccountTemplate
        (
            name: AccountName.Administrator,
            rolesToAssign: new [] { Role.Admin, Role.User },
            spacesToCreate: new [] { SpaceTemplate.Configuration, SpaceTemplate.Data  }
        );


        public string Name => _name;
        private readonly string _name;
        public string[] RolesToAssign => _rolesToAssign;
        private readonly string[] _rolesToAssign;

        public SpaceTemplate[] SpacesToCreate => _spacesToCreate;
        private readonly SpaceTemplate[] _spacesToCreate;

        private AccountTemplate(
            string name,
            string[] rolesToAssign, 
            SpaceTemplate[] spacesToCreate)
        {
            _name = name;
            _rolesToAssign = rolesToAssign;
            _spacesToCreate = spacesToCreate;
        }
    }
}