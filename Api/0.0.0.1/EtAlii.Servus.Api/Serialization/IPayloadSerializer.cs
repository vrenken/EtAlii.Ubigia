namespace EtAlii.Servus.Api
{
    public interface IPayloadSerializer
    {
        byte[] Serialize(object value);
        T Deserialize<T>(byte[] bytes);
    }
}
