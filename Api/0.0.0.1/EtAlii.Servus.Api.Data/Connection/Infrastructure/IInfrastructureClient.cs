namespace EtAlii.Servus.Api
{
    using System.Net;

    public interface IInfrastructureClient
    {
        string AuthenticationToken { get; set; }

        T Get<T>(string address, ICredentials credentials = null);

        T Post<T>(string address, T value = null, ICredentials credentials = null)
            where T : class;

        U Post<T, U>(string address, T value = null, ICredentials credentials = null)
            where T : class
            where U : class;

        void Delete(string address, ICredentials credentials = null);

        T Put<T>(string address, T value, ICredentials credentials = null);
    }
}
