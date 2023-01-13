// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System.Threading.Tasks;

public interface IInformationDataClient : IStorageTransportClient
{
    Task<Storage> GetConnectedStorage(IStorageConnection connection);
    Task<ConnectivityDetails> GetConnectivityDetails(IStorageConnection connection);
}

public interface IInformationDataClient<in TTransport> : IInformationDataClient, IStorageTransportClient<TTransport>
    where TTransport : IStorageTransport
{
}
