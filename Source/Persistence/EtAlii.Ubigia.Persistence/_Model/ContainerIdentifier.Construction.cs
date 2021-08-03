// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    public readonly partial struct ContainerIdentifier
    {
        private ContainerIdentifier(string[] paths)
        {
            Paths = paths;
        }

        internal static ContainerIdentifier FromPaths(params string[] paths)
        {
            return new(paths);
        }
    }
}
