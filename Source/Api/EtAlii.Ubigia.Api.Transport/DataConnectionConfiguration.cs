// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public class DataConnectionConfiguration : ConfigurationBase, IDataConnectionConfiguration, IEditableDataConnectionConfiguration
    {
        ITransportProvider IEditableDataConnectionConfiguration.TransportProvider { get => TransportProvider; set => TransportProvider = value; }
        public ITransportProvider TransportProvider { get; private set; }

        Func<IDataConnection> IEditableDataConnectionConfiguration.FactoryExtension { get => FactoryExtension; set => FactoryExtension = value; }
        public Func<IDataConnection> FactoryExtension { get; private set; }

        Uri IEditableDataConnectionConfiguration.Address { get => Address; set => Address = value; }
        public Uri Address { get; private set; }

        string IEditableDataConnectionConfiguration.AccountName { get => AccountName; set => AccountName = value; }
        public string AccountName { get; private set; }

        string IEditableDataConnectionConfiguration.Password { get => Password; set => Password = value; }
        public string Password { get; private set; }

        string IEditableDataConnectionConfiguration.Space { get => Space; set => Space = value; }
        public string Space { get; private set; }
    }
}
