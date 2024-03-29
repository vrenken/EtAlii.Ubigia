// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

public interface IStorageConnectionFactory
{
    IStorageConnection Create(IStorageConnectionOptions options);
}
