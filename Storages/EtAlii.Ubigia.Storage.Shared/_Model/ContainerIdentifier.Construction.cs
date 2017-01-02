namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Storage;
    using System;

    public partial struct ContainerIdentifier
    {
        private ContainerIdentifier(string[] paths)
        {
            _paths = paths;
        }

        internal static ContainerIdentifier FromPaths(params string[] paths)
        {
            return new ContainerIdentifier(paths);
        }
    }
}