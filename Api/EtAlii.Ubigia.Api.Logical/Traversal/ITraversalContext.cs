namespace EtAlii.Ubigia.Api.Logical
{
    public interface ITraversalContext
    {
        ITraversalContextRootSet Roots { get; }
        ITraversalContextEntrySet Entries { get; }
        ITraversalContextPropertySet Properties { get; }
    }
}