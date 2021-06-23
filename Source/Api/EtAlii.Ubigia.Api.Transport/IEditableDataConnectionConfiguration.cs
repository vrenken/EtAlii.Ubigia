// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface IEditableDataConnectionConfiguration
    {
        ITransportProvider TransportProvider { get; set; }

        Func<IDataConnection> FactoryExtension { get; set; }

        Uri Address { get; set; }

        string AccountName { get; set; }

        string Password { get; set; }

        string Space { get; set; }
    }
}