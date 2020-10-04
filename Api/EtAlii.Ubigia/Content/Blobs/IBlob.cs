namespace EtAlii.Ubigia
{
    public interface IBlob
    {
        bool Stored { get; }
        ulong TotalParts { get; set; }

        BlobSummary Summary { get; }
    }
}
