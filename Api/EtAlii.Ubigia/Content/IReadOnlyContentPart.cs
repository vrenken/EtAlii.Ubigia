namespace EtAlii.Ubigia
{
    public interface IReadOnlyContentPart : IBlobPart
    {
        byte[] Data { get; }
    }
}