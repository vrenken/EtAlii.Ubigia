﻿namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;

    public interface IContentDefinitionGetter
    {
        Task<ContentDefinition> Get(Identifier identifier);
    }
}