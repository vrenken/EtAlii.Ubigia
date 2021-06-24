// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface IStorageClientContext
    {
        Task Open(IStorageConnection storageConnection);
        Task Close(IStorageConnection storageConnection);
    }
}
