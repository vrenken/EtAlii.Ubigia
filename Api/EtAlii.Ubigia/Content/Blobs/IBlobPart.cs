namespace EtAlii.Ubigia
{
    public interface IBlobPart
    {
        bool Stored { get; }
        ulong Id { get; } 
    }
}
