namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.ObjectModel;

    public interface IConfigurationDetails
    {
        ReadOnlyDictionary<string, string> Folders { get; }
        ReadOnlyDictionary<string, string> Hosts { get; }
        ReadOnlyDictionary<string, int> Ports { get; }
        ReadOnlyDictionary<string, string> Paths { get; }
    }
}