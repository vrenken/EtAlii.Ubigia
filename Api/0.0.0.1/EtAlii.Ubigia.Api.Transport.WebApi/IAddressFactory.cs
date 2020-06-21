﻿namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;

    public interface IAddressFactory
    {
        Uri Create(Uri baseAddress, string relativeAddress, params string[] parameters);
        Uri Create(Storage storage, string relativeAddress, params string[] parameters);
        Uri Create(ISpaceTransport transport, string relativeAddress, params string[] parameters);
        Uri Create(IStorageTransport transport, string relativeAddress, params string[] parameters);
    }
}
