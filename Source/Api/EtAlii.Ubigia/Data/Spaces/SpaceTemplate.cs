// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class SpaceTemplate
    {
        public static SpaceTemplate[] All { get; }
        public static SpaceTemplate Data { get; }
        public static SpaceTemplate System { get; }
        public static SpaceTemplate Configuration { get; }
        public static SpaceTemplate Metrics { get; }

        public string Name { get; }

        public string[] RequiredRoles { get; }

        public string[] RootsToCreate { get; }

        public string[] SetupScript { get; }

        static SpaceTemplate()
        {
            Data = new SpaceTemplate
            (
                name: SpaceName.Data,
                requiredRoles: new [] { Role.User },
                rootsToCreate: new[]
                {
                    "Tail",
                    "Hierarchy",
                    "Sequences",
                    "Tag",
                    "Time",
                    "Communication",
                    "Person",
                    "Location",
                    "Subscription",
                    "Head",
                },
                setupScript: new[] { "" }
            );
            System = new SpaceTemplate
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
            Configuration = new SpaceTemplate
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
            Metrics = new SpaceTemplate
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
            All = new[]
            {
                Data,
                System,
                Configuration,
                Metrics
            };
        }

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
