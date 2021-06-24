// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    public interface IHubProxyMethodInvoker
    {
        IAsyncEnumerable<T> Stream<T>(HubConnection connection, string proxyName, string methodName, params object[] parameters)
            where T: class;
        
        Task<T> Invoke<T>(HubConnection connection, string proxyName, string methodName, params object[] parameters);
        Task Invoke(HubConnection connection, string proxyName, string methodName, params object[] parameters);
    }
}