namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public class AccountTemplate
    {
        public static AccountTemplate[] All { get { return _all.Value; } }
        private static readonly Lazy<AccountTemplate[]> _all = new Lazy<AccountTemplate[]>(() => new [] {
            User,
            System,
            Administrator
        });

        public static AccountTemplate User { get { return _user; } }
        private static readonly AccountTemplate _user = new AccountTemplate
        (
            name: AccountName.User, 
            rolesToAssign: new [] { Role.User, },
            spacesToCreate: new []{ SpaceTemplate.Configuration, SpaceTemplate.Data }
        );

        public static AccountTemplate System { get { return _system; } }
        private static readonly AccountTemplate _system = new AccountTemplate
        (
            name: AccountName.System,
            rolesToAssign: new [] { Role.System },
            spacesToCreate: new [] { SpaceTemplate.System, SpaceTemplate.Metrics }
        );

        public static AccountTemplate Administrator { get { return _administrator; } }
        private static readonly AccountTemplate _administrator = new AccountTemplate
        (
            name: AccountName.Administrator,
            rolesToAssign: new [] { Role.Admin, Role.User },
            spacesToCreate: new [] { SpaceTemplate.Configuration, SpaceTemplate.Data  }
        );


        public string Name { get { return _name; } }
        private readonly string _name;
        public string[] RolesToAssign { get { return _rolesToAssign; } }
        private readonly string[] _rolesToAssign;

        public SpaceTemplate[] SpacesToCreate { get { return _spacesToCreate; } }
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