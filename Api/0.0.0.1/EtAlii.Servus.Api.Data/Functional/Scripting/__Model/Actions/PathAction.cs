namespace EtAlii.Servus.Api.Data
{
    public abstract class PathAction : Action
    {
        public Path Path { get; private set; }

        public PathAction(Path path)
        {
            Path = path;
        }
    }
}
