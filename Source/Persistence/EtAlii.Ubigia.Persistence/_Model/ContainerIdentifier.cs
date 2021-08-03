// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System;
    using System.IO;

    public readonly partial struct ContainerIdentifier
    {
        public string[] Paths { get; }

        public static readonly ContainerIdentifier Empty = new(Array.Empty<string>());


        public static ContainerIdentifier Combine(ContainerIdentifier container, params string[] additionalPaths)
        {
            var paths = new string[container.Paths.Length + additionalPaths.Length];
            var containerPaths = container.Paths;
            Array.Copy(containerPaths, 0, paths, 0, containerPaths.Length);
            Array.Copy(additionalPaths, 0, paths, containerPaths.Length, additionalPaths.Length);
            return new ContainerIdentifier(paths);
        }

        public override string ToString()
        {
            if(this == Empty || Paths.Length == 0)
            {
                return $"{GetType().Name}.Empty";
            }
            else
            {
                return string.Join(Path.DirectorySeparatorChar, Paths);
            }
        }
    }
}
