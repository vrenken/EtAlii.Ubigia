namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.IO;

    public class ContentPartQueryHandler : IContentPartQueryHandler
    {
        private readonly IDataConnection _connection;

        public ContentPartQueryHandler(IDataConnection connection)
        {
            _connection = connection;
        }

        public void Execute(ContentPartQuery query)
        {
            var totalParts = query.Content.TotalParts;
            for (UInt64 part = 0; part < totalParts; part++)
            {
                GetContentPart(query.Stream, query.Identifier, query.Content, part);
            }
        }

        private void GetContentPart(Stream localDataStream, Identifier identifier, IReadOnlyContent content, UInt64 contentPartId)
        {
            var contentPart = _connection.Content.Retrieve(identifier, contentPartId);
            var buffer = contentPart.Data;
            localDataStream.Write(buffer, 0, (int)buffer.Length);
        }
    }
}
