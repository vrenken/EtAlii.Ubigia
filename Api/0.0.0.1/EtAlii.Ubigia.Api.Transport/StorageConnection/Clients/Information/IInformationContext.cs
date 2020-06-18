namespace EtAlii.Ubigia.Api.Transport
{
    public interface IInformationContext : IStorageClientContext
    {
        IInformationDataClient Data { get; }
    }
}