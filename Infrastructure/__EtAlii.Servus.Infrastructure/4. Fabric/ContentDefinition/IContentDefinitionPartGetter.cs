namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using EtAlii.Servus.Api;

    public interface IContentDefinitionPartGetter
    {
        IReadOnlyContentDefinitionPart Get(Identifier identifier, UInt64 contentDefinitionPartId);
    }
}