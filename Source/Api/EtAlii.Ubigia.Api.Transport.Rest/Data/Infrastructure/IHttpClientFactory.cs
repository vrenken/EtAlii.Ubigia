// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System;
    using System.Net;
    using System.Net.Http;

    public interface IHttpClientFactory
    {
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
