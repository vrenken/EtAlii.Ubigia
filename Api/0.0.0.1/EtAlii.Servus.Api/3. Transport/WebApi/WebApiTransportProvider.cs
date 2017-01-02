namespace EtAlii.Servus.Api.Transport.WebApi
{
    public class WebApiTransportProvider : ITransportProvider
    {
        public static WebApiTransportProvider Create()
        {
            return new WebApiTransportProvider();
        }

        public ISpaceTransport GetSpaceTransport()
        {
            return new WebApiSpaceTransport();
        }
    }
}