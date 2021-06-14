namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System;
    using System.Net;
    using System.Net.Http;

    public interface IHttpClientFactory
    {
        // TODO: Usage of these client probably can be cached. However, if we do so the corresponding correlation headers should be applied differently as well.
        /// <summary>
        /// Create a new HttpClient, configured to access the Ubigia storage specified.
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="hostIdentifier"></param>
        /// <param name="authenticationToken"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        HttpClient Create(ICredentials credentials, string hostIdentifier, string authenticationToken, Uri address);
    }
}
