// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public interface ISystemConnection : IDisposable
    {
        Task<(IDataConnection, DataConnectionOptions)> OpenSpace(string accountName, string spaceName);

        Task<IManagementConnection> OpenManagementConnection();
    }
}
