namespace EtAlii.Servus.Api.Transport
{
    public interface IPayloadSerializer
    {
        byte[] Serialize(object value);
        T Deserialize<T>(byte[] bytes);
    }
}
