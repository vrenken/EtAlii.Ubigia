﻿namespace EtAlii.Servus.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Fabric;

    public class ContentQueryHandler : IContentQueryHandler
    {
        private readonly IFabricContext _fabric;

        public ContentQueryHandler(IFabricContext fabric)
        {
            _fabric = fabric;
        }

        public async Task<IReadOnlyContent> Execute(ContentQuery query)
        {
            var content = await _fabric.Content.Retrieve(query.Identifier);
            return content;
        }
    }
}
