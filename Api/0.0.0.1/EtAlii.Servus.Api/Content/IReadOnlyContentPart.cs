namespace EtAlii.Servus.Api
{
    public interface IReadOnlyContentPart : IBlobPart
    {
        byte[] Data { get; }
    }
}