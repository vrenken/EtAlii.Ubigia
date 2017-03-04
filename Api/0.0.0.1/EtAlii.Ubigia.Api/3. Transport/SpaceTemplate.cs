namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public class SpaceTemplate
    {
        public static SpaceTemplate[] All => _all.Value;

        private static readonly Lazy<SpaceTemplate[]> _all = new Lazy<SpaceTemplate[]>(() => new[] {
            Data,
            System,
            Configuration,
            Metrics
        });

        public static SpaceTemplate Data => _data;

        private static readonly SpaceTemplate _data = new SpaceTemplate
        (
            name: SpaceName.Data,
            requiredRoles: new [] { Role.User },
            rootsToCreate: new[] 
            {
                "Tail",
                "Hierarchy",
                "Sequences",
                "Tags",
                "Time",
                "Communications",
                "Person",
                "Locations",
                "Subscriptions",
                "Head",
            },
            setupScript: new[] { "" }
        );

        public static SpaceTemplate System => _system;

        private static readonly SpaceTemplate _system = new SpaceTemplate
        (
            name: SpaceName.System,
            requiredRoles: new [] { Role.System, },
            rootsToCreate: new[]
            {
                "Tail",
                "Providers",
                "Users",
                "Spaces",
                "Diagnostics",
                "Time",
                "Head",
            },
            setupScript: new [] { "" }
        );

        public static SpaceTemplate Configuration => _configuration;

        private static readonly SpaceTemplate _configuration = new SpaceTemplate
        (
            name: SpaceName.Configuration,
            requiredRoles: new [] { Role.User },
            rootsToCreate: new[]
            {
                "Tail",
                "Providers",
                "Time",
                "Head",
            },
            setupScript: new [] { "" }
        );

        public static SpaceTemplate Metrics => _metrics;

        private static readonly SpaceTemplate _metrics = new SpaceTemplate
        (
            name: SpaceName.Metrics,
            requiredRoles: new [] {Role.User },
            rootsToCreate: new[]
            {
                "Tail",
                "Providers",
                "Users",
                "Spaces",
                "Time",
                "Head",
            },
            setupScript: new[] { "" }
        );


        public string Name => _name;
        private readonly string _name;
        public string[] RequiredRoles => _requiredRoles;
        private readonly string[] _requiredRoles;

        public string[] RootsToCreate => _rootsToCreate;
        private readonly string[] _rootsToCreate;

        public string[] SetupScript => _setupScript;
        private readonly string[] _setupScript;

        private SpaceTemplate(
            string name,
            string[] requiredRoles, 
            string[] rootsToCreate, 
            string[] setupScript)
        {
            _name = name;
            _requiredRoles = requiredRoles;
            _rootsToCreate = rootsToCreate;
            _setupScript = setupScript;
        }
    }
}