namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public static class DataConnectionConfigurationUseExtensions
    {
        public static TDataConnectionConfiguration Use<TDataConnectionConfiguration>(this TDataConnectionConfiguration configuration, Func<IDataConnection> factoryExtension)
            where TDataConnectionConfiguration : DataConnectionConfiguration
        {
            var editableConfiguration = (IEditableDataConnectionConfiguration) configuration;

            editableConfiguration.FactoryExtension = factoryExtension;
            return configuration;
        }

        public static TDataConnectionConfiguration UseTransport<TDataConnectionConfiguration>(this TDataConnectionConfiguration configuration, ITransportProvider transportProvider)
            where TDataConnectionConfiguration : DataConnectionConfiguration
        {
            var editableConfiguration = (IEditableDataConnectionConfiguration) configuration;
            
            if (editableConfiguration.TransportProvider != null)
            {
                throw new ArgumentException("A TransportProvider has already been assigned to this DataConnectionConfiguration",
                    nameof(transportProvider));
            }

            editableConfiguration.TransportProvider = transportProvider ?? throw new ArgumentNullException(nameof(transportProvider));

            return configuration;
        }

        public static TDataConnectionConfiguration Use<TDataConnectionConfiguration>(this TDataConnectionConfiguration configuration, Uri address)
            where TDataConnectionConfiguration : DataConnectionConfiguration
        {
            var editableConfiguration = (IEditableDataConnectionConfiguration) configuration;
			
            if (editableConfiguration.Address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this DataConnectionConfiguration");
            }

            editableConfiguration.Address = address ?? throw new ArgumentNullException(nameof(address));
            return configuration;
        }

        public static TDataConnectionConfiguration Use<TDataConnectionConfiguration>(this TDataConnectionConfiguration configuration, string accountName, string space, string password)
            where TDataConnectionConfiguration : DataConnectionConfiguration
        {
            var editableConfiguration = (IEditableDataConnectionConfiguration) configuration;
            
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException(nameof(accountName));
            }
            if (editableConfiguration.AccountName != null)
            {
                throw new InvalidOperationException("An accountName has already been assigned to this DataConnectionConfiguration");
            }
            //if [string.IsNullOrWhiteSpace[password]]
            //[
            //    throw new ArgumentException(nameof(password))
            //]
            if (editableConfiguration.Password != null)
            {
                throw new InvalidOperationException("A password has already been assigned to this DataConnectionConfiguration");
            }
            if (string.IsNullOrWhiteSpace(space))
            {
                throw new ArgumentException(nameof(space));
            }
            if (editableConfiguration.Space != null)
            {
                throw new InvalidOperationException("A space has already been assigned to this DataConnectionConfiguration");
            }

            editableConfiguration.AccountName = accountName;
            editableConfiguration.Password = password;
            editableConfiguration.Space = space;
            return configuration;
        }
    }
}