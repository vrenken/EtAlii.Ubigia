namespace EtAlii.Ubigia.Persistence
{
    public partial struct ContainerIdentifier
    {
        private ContainerIdentifier(string[] paths)
        {
            Paths = paths;
        }

        internal static ContainerIdentifier FromPaths(params string[] paths)
        {
            return new ContainerIdentifier(paths);
        }
    }
}