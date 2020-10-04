namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;

    public interface IContentDefinitionPartGetter
    {
        IReadOnlyContentDefinitionPart Get(Identifier identifier, UInt64 contentDefinitionPartId);
    }
}