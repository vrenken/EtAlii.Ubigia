// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    public class ContentPartCalculator : IContentPartCalculator
    {
        private const ulong _bytesInMegaByte = 1024 * 1024;

        public ulong GetRequiredParts(ulong totalBytes)
        {
            double partSize = GetPartSize(totalBytes);
            var bytes = totalBytes % partSize;
            var megaBytes = (totalBytes - bytes) / partSize;
            megaBytes = bytes > 0 ? megaBytes + 1 : megaBytes;

            return (ulong)megaBytes;
        }

        public ulong GetPartSize(ulong totalBytes)
        {
            return _bytesInMegaByte;
        }


        public ulong GetPart(ulong totalBytes, ulong positionInBytes)
        {
            double partSize = GetPartSize(totalBytes);
            var bytes = positionInBytes % partSize;
            var megaBytes = (positionInBytes - bytes) / partSize;

            return (ulong)megaBytes;
        }
    }
}
