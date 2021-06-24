// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using HashLib;

    public class ContentPartStoreCommandHandler : IContentPartStoreCommandHandler
    {
        private readonly IFabricContext _fabric;
        private readonly IHash _hash;

        public ContentPartStoreCommandHandler(
            IFabricContext fabric,
            IHash hash)
        {
            _fabric = fabric;
            _hash = hash;
        }

        public async Task Execute(ContentPartStoreCommand command)
        {
            var dataBuffer = new byte[command.PartSize];

            for (ulong part = 0; part < command.RequiredParts; part++)
            {
                var bytesToWrite = await GetBytesToWrite(command.Stream, command.PartSize, dataBuffer, part).ConfigureAwait(false);
                var hasResult = _hash.ComputeBytes(bytesToWrite);
                var checksum = hasResult.GetULong();
                await StoreContentDefinitionPart(command.PartSize, command.Identifier, command.ContentDefinition, part, checksum).ConfigureAwait(false);
                await StoreContentPart(command.Identifier, command.Content, bytesToWrite, part).ConfigureAwait(false);
            }
        }

        private async Task StoreContentPart(
            Identifier identifier,
            Content content,
            byte[] bytesToWrite,
            ulong part)
        {
            var contentPartAlreadyAvailable = false;
            if (content.Summary != null)
            {
                contentPartAlreadyAvailable = content.Summary.AvailableParts.Contains(part);
            }
            if (!contentPartAlreadyAvailable)
            {
                var contentPart = new ContentPart
                {
                    Id = part,
                    Data = bytesToWrite
                };
                await _fabric.Content.Store(identifier, contentPart).ConfigureAwait(false);
            }
        }

        private async Task<byte[]> GetBytesToWrite(Stream stream, ulong partSize, byte[] dataBuffer, ulong part)
        {
            var offset = part * partSize;
            stream.Seek((long)offset, SeekOrigin.Begin);
#pragma warning disable CA1835 // No way to elegantly use Memory instances here without breaking apart the complete HasLib implementation.
            var bytesRead = await stream.ReadAsync(dataBuffer, 0, (int)partSize).ConfigureAwait(false);
#pragma warning restore CA1835
            var bytesToWrite = new byte[bytesRead];
            Array.Copy(dataBuffer, bytesToWrite, bytesRead);
            return bytesToWrite;
        }

        private async Task StoreContentDefinitionPart(
            ulong partSize,
            Identifier identifier,
            ContentDefinition contentDefinition,
            ulong part,
            ulong checksum)
        {
            var contentDefinitionPartAlreadyAvailable = false;
            if (contentDefinition.Summary != null)
            {
                contentDefinitionPartAlreadyAvailable = contentDefinition.Summary.AvailableParts.Contains(part);
            }
            if (!contentDefinitionPartAlreadyAvailable)
            {
                var contentDefinitionPart = new ContentDefinitionPart
                {
                    Id = part,
                    Size = partSize,
                    Checksum = checksum,
                };
                await _fabric.Content.StoreDefinition(identifier, contentDefinitionPart).ConfigureAwait(false);
            }
        }
    }
}
