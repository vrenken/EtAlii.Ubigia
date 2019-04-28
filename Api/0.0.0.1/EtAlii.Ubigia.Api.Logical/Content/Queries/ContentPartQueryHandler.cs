namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    public class ContentPartQueryHandler : IContentPartQueryHandler
    {
        private readonly IFabricContext _fabric;

        public ContentPartQueryHandler(IFabricContext fabric)
        {
            _fabric = fabric;
        }

        public async Task Execute(ContentPartQuery query)
        {
            var totalParts = query.Content.TotalParts;
            for (UInt64 part = 0; part < totalParts; part++)
            {
                await GetContentPart(query.Stream, query.Identifier, part); // , query.Content
            }
        }

        private async Task GetContentPart(Stream localDataStream, Identifier identifier, UInt64 contentPartId) // , IReadOnlyContent content
        {
            var contentPart = await _fabric.Content.Retrieve(identifier, contentPartId);
            var buffer = contentPart.Data;
            localDataStream.Write(buffer, 0, buffer.Length);
        }
    }
}
