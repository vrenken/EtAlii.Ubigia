namespace EtAlii.Servus.Api
{
    using System;

    public interface IReadOnlyContentDefinitionPart : IBlobPart
    {
        //UInt64 Id { get; }
        UInt64 Checksum { get; }
        UInt64 Size { get; }
    }
}