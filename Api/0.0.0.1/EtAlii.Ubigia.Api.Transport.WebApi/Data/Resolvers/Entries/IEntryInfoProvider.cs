namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    public interface IEntryInfoProvider
    {
        Entry Entry { get; }
        Identifier EntryId { get; }

        Storage TargetStorage { get; }
    }
}
