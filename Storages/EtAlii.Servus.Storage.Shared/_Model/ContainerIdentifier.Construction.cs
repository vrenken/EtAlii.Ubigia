namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Storage;
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