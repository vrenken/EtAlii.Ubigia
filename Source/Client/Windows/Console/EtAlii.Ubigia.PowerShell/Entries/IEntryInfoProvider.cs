namespace EtAlii.Ubigia.PowerShell.Entries
{
    public interface IEntryInfoProvider
    {
        Entry Entry { get; }
        Identifier EntryId { get; }

        Storage TargetStorage { get; }
    }
}
