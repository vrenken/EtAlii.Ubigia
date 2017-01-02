namespace EtAlii.Ubigia.Api
{
    public interface IReadOnlyContentPart : IBlobPart
    {
        byte[] Data { get; }
    }
}