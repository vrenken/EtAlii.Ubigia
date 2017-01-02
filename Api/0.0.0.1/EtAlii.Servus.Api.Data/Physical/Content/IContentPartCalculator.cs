namespace EtAlii.Servus.Api.Data
{
    using System;

    public interface IContentPartCalculator
    {
        UInt64 GetRequiredParts(UInt64 totalBytes);
        UInt64 GetPartSize(UInt64 totalBytes);
        UInt64 GetPart(UInt64 totalBytes, UInt64 positionInBytes);
    }
}
