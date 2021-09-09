// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;

    [Obsolete]
    public interface IManagementConnectionFactory
    {
        IManagementConnection Create(ManagementConnectionOptions options);
    }
}
