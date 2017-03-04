namespace EtAlii.Ubigia.Storage
{
    using System;

    public partial struct ContainerIdentifier
    {
        public string[] Paths => _paths;
        private readonly string[] _paths;

        public static readonly ContainerIdentifier Empty = new ContainerIdentifier(new string[]{});


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
            if(this == ContainerIdentifier.Empty || _paths.Length == 0)
            {
                return String.Format("{0}.Empty", this.GetType().Name);
            }
            else
            {
                return String.Join("\\", _paths);
            }
        }
    }
}
