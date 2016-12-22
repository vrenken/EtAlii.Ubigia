﻿namespace EtAlii.Servus.Api.Logical
{
    using System.Threading.Tasks;

    public interface IContentDefinitionQueryHandler
    {
        Task<IReadOnlyContentDefinition> Execute(ContentDefinitionQuery query);
    }
}
