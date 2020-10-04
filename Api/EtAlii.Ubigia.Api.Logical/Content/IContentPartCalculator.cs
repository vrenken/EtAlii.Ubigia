namespace EtAlii.Ubigia.Api.Logical
{
    public interface IContentPartCalculator
    {
        ulong GetRequiredParts(ulong totalBytes);
        ulong GetPartSize(ulong totalBytes);
        ulong GetPart(ulong totalBytes, ulong positionInBytes);
    }
}
