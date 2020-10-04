namespace EtAlii.Ubigia
{
    public interface IReadOnlyContentDefinitionPart : IBlobPart
    {
        //UInt64 Id { get; }
        ulong Checksum { get; }
        ulong Size { get; }
    }
}