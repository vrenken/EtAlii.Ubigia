namespace EtAlii.Ubigia.Storage
{
    public interface IPathBuilder
    {
        string BaseFolder { get; }
        string GetFolder(ContainerIdentifier container);
        string GetFileName(string fileId, ContainerIdentifier container);

        string GetFileNameWithoutExtension(string path);
        string Combine(string path1, string path2);
        string GetDirectoryName(string path);
    }
}
