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

            for (UInt64 part = 0; part < command.RequiredParts; part++)
            {
                var bytesToWrite = await GetBytesToWrite(command.Stream, command.PartSize, dataBuffer, part);
                var hasResult = _hash.ComputeBytes(bytesToWrite);
                var checksum = hasResult.GetULong();
                await StoreContentDefinitionPart(command.PartSize, command.Identifier, command.ContentDefinition, part, checksum);
                await StoreContentPart(command.Identifier, command.Content, bytesToWrite, part);
            }
        }

        private async Task StoreContentPart(
            Identifier identifier, 
            IReadOnlyContent content,
            byte[] bytesToWrite,
            UInt64 part)
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
                    Data = bytesToWrite,
                };
                await _fabric.Content.Store(identifier, contentPart);
            }
        }

        private async Task<byte[]> GetBytesToWrite(Stream stream, UInt64 partSize, byte[] dataBuffer, UInt64 part)
        {
            var offset = part * partSize;
            stream.Seek((long)offset, SeekOrigin.Begin);
            var bytesRead = await stream.ReadAsync(dataBuffer, 0, (int)partSize);
            var bytesToWrite = new byte[bytesRead];
            Array.Copy(dataBuffer, bytesToWrite, bytesRead);
            return bytesToWrite;
        }

        private async Task StoreContentDefinitionPart(
            UInt64 partSize, 
            Identifier identifier, 
            IReadOnlyContentDefinition contentDefinition, 
            UInt64 part,
            UInt64 checksum)
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
                await _fabric.Content.StoreDefinition(identifier, contentDefinitionPart);
            }
        }
    }
}
