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

        public static SpaceTemplate Data { get; } = new SpaceTemplate
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

        public static SpaceTemplate System { get; } = new SpaceTemplate
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

        public static SpaceTemplate Configuration { get; } = new SpaceTemplate
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

        public static SpaceTemplate Metrics { get; } = new SpaceTemplate
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


        public string Name { get; }

        public string[] RequiredRoles { get; }

        public string[] RootsToCreate { get; }

        public string[] SetupScript { get; }

        private SpaceTemplate(
            string name,
            string[] requiredRoles, 
            string[] rootsToCreate, 
            string[] setupScript)
        {
            Name = name;
            RequiredRoles = requiredRoles;
            RootsToCreate = rootsToCreate;
            SetupScript = setupScript;
        }
    }
}