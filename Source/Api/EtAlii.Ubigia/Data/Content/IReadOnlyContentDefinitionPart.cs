namespace EtAlii.Ubigia
{
    public interface IReadOnlyContentDefinitionPart : IBlobPart
    {
        ulong Checksum { get; }
        ulong Size { get; }
    }
}