// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    public interface IRestInfrastructureClient
    {
        string AuthenticationToken { get; set; }

        Task<TResult> Get<TResult>(Uri address, ICredentials credentials = null);

        Task<TValue> Post<TValue>(Uri address, TValue value = null, ICredentials credentials = null)
            where TValue : class;

        Task<TResult> Post<TValue, TResult>(Uri address, TValue value = null, ICredentials credentials = null)
            where TValue : class
            where TResult : class;

        Task Delete(Uri address, ICredentials credentials = null);

        Task<TValue> Put<TValue>(Uri address, TValue value, ICredentials credentials = null);
    }
}
