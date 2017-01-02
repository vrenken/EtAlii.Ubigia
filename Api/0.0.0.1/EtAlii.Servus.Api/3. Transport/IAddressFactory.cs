namespace EtAlii.Servus.Api.Transport
{
    public interface IAddressFactory
    {
        string CreateFullAddress(string address, params string[] fragments);
        string Create(Storage storage, string path, params string[] parameters);
    }
}
