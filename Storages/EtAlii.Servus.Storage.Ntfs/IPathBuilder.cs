namespace EtAlii.Servus.Storage
{
    public interface IPathBuilder
    {
        string BaseFolder { get; }
        string GetFolder(ContainerIdentifier container);
        string GetFileName(string fileId, ContainerIdentifier container);
    }
}
