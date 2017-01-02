namespace EtAlii.Ubigia.Api.Management
{
    public interface IEntryInfoProvider
    {
        Entry Entry { get; }
        Identifier EntryId { get; }

        Storage TargetStorage { get; }
    }
}
