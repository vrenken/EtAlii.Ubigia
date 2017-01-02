namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using HashLib;
    using System;
    using System.IO;
    using System.Linq;

    public class ContentPartStoreCommandHandler : IContentPartStoreCommandHandler
    {
        private readonly IDataConnection _connection;
        private readonly IHash _hash;

        public ContentPartStoreCommandHandler(
            IDataConnection connection,
            IHash hash)
        {
            _connection = connection;
            _hash = hash;
        }

        public void Execute(ContentPartStoreCommand command)
        {
            var dataBuffer = new byte[command.PartSize];

            for (UInt64 part = 0; part < command.RequiredParts; part++)
            {
                var bytesToWrite = GetBytesToWrite(command.Stream, command.PartSize, dataBuffer, part);
                var hasResult = _hash.ComputeBytes(bytesToWrite);
                var checksum = hasResult.GetULong();
                StoreContentDefinitionPart(command.PartSize, command.Identifier, command.ContentDefinition, part, checksum);
                StoreContentPart(command.Identifier, command.Content, bytesToWrite, part);
            }
        }

        private void StoreContentPart(
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
                _connection.Content.Store(identifier, contentPart);
            }
        }

        private byte[] GetBytesToWrite(Stream stream, UInt64 partSize, byte[] dataBuffer, UInt64 part)
        {
            var offset = part * partSize;
            stream.Seek((long)offset, SeekOrigin.Begin);
            var bytesRead = stream.Read(dataBuffer, 0, (int)partSize);
            var bytesToWrite = new byte[bytesRead];
            Array.Copy(dataBuffer, bytesToWrite, bytesRead);
            return bytesToWrite;
        }

        private void StoreContentDefinitionPart(
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
                _connection.Content.StoreDefinition(identifier, contentDefinitionPart);
            }
        }
    }
}
