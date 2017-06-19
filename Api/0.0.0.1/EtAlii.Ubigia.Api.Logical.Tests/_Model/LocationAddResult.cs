namespace EtAlii.Ubigia.Api.Tests
{
    public class LocationAddResult
    {

        public readonly string Path;
        public readonly IEditableEntry Entry;

        public LocationAddResult(string path, IEditableEntry entry)
        {
            Path = path;
            Entry = entry;
        }
    }
}