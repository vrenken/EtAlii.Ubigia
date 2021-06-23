// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface ISpaceTransportClient
    {
        Task Connect(ISpaceConnection spaceConnection);
        Task Disconnect(); 
    }

    public interface ISpaceTransportClient<in TTransport> : ISpaceTransportClient
        where TTransport: ISpaceTransport
    {
        Task Connect(ISpaceConnection<TTransport> spaceConnection);
    }
}
