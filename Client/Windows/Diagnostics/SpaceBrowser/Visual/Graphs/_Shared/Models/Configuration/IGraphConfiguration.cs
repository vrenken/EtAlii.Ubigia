namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    public interface IGraphConfiguration
    {
        bool ShowHierarchical { get; set; }
        bool ShowSequential { get; set; }
        bool ShowTemporal { get; set; }
        bool AddNewEntries { get; set; }
        bool ExpandNewEntries { get; set; }
    }
}