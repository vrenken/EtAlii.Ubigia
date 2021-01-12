namespace EtAlii.Ubigia.Api.Logical
{
    public interface IPathTraversalContext
    {
        ITraversalContextRootSet Roots { get; }
        ITraversalContextEntrySet Entries { get; }
        ITraversalContextPropertySet Properties { get; }
    }
}
