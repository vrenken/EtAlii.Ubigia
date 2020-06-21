namespace EtAlii.Ubigia.PowerShell.Entries
{
    using EtAlii.Ubigia.Api;

    public interface IEntryInfoProvider
    {
        Entry Entry { get; }
        Identifier EntryId { get; }

        Storage TargetStorage { get; }
    }
}
