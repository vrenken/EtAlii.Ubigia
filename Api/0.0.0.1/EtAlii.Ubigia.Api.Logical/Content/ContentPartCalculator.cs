namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    public class ContentPartCalculator : IContentPartCalculator
    {
        private const UInt64 BytesInMegaByte = 1024 * 1024;

        public UInt64 GetRequiredParts(UInt64 totalBytes)
        {
            double partSize = GetPartSize(totalBytes);
            var bytes = totalBytes % partSize;
            var megaBytes = (totalBytes - bytes) / partSize;
            megaBytes = bytes > 0 ? megaBytes + 1 : megaBytes;

            return (UInt64)megaBytes;
        }

        public UInt64 GetPartSize(UInt64 totalBytes)
        {
            return BytesInMegaByte;
        }


        public UInt64 GetPart(UInt64 totalBytes, UInt64 positionInBytes)
        {
            double partSize = GetPartSize(totalBytes);
            var bytes = positionInBytes % partSize;
            var megaBytes = (positionInBytes - bytes) / partSize;

            return (UInt64)megaBytes;
        }
    }
}
