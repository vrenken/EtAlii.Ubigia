namespace EtAlii.Servus.Api
{
    using System.Net;

    public interface IInfrastructureClient
    {
        string AuthenticationToken { get; set; }

        TResult Get<TResult>(string address, ICredentials credentials = null);

        TValue Post<TValue>(string address, TValue value = null, ICredentials credentials = null)
            where TValue : class;

        TResult Post<TValue, TResult>(string address, TValue value = null, ICredentials credentials = null)
            where TValue : class
            where TResult : class;

        void Delete(string address, ICredentials credentials = null);

        TValue Put<TValue>(string address, TValue value, ICredentials credentials = null);
    }
}
