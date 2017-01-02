namespace EtAlii.Servus.Api.Logical
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Fabric;

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
                await GetContentPart(query.Stream, query.Identifier, query.Content, part);
            }
        }

        private async Task GetContentPart(Stream localDataStream, Identifier identifier, IReadOnlyContent content, UInt64 contentPartId)
        {
            var contentPart = await _fabric.Content.Retrieve(identifier, contentPartId);
            var buffer = contentPart.Data;
            localDataStream.Write(buffer, 0, (int)buffer.Length);
        }
    }
}
