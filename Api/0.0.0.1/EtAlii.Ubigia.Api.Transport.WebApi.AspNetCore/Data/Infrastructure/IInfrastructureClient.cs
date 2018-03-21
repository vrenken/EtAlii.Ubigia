namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System.Net;
    using System.Threading.Tasks;

    // TODO: The IInfrastructureClient should be made internal and only used Web Api related code.
    public interface IInfrastructureClient
    {
        string AuthenticationToken { get; set; }

        Task<TResult> Get<TResult>(string address, ICredentials credentials = null);

        Task<TValue> Post<TValue>(string address, TValue value = null, ICredentials credentials = null)
            where TValue : class;

        Task<TResult> Post<TValue, TResult>(string address, TValue value = null, ICredentials credentials = null)
            where TValue : class
            where TResult : class;

        Task Delete(string address, ICredentials credentials = null);

        Task<TValue> Put<TValue>(string address, TValue value, ICredentials credentials = null);
    }
}
