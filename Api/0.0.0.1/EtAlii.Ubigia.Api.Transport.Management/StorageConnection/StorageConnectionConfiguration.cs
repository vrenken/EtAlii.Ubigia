namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;

    public class StorageConnectionConfiguration : IStorageConnectionConfiguration
    {
        public IStorageTransport Transport { get; private set; }

        public Uri Address { get; private set; }

        public string AccountName { get; private set; }

        public string Password { get; private set; }

        public IStorageConnectionExtension[] Extensions { get; private set; }

        public StorageConnectionConfiguration()
        {
            Extensions = new IStorageConnectionExtension[0];
        }

        public IStorageConnectionConfiguration Use(IStorageConnectionExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IStorageConnectionConfiguration Use(IStorageTransport transport)
        {
            if (transport == null)
            {
                throw new ArgumentException(nameof(transport));
            }
            if (Transport != null)
            {
                throw new InvalidOperationException("A Transport has already been assigned to this StorageConnectionConfiguration");
            }

            Transport = transport;
            return this;
        }

        public IStorageConnectionConfiguration Use(Uri address)
        {
			if (Address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this StorageConnectionConfiguration");
            }

            Address = address ?? throw new ArgumentNullException(nameof(address));
            return this;
        }
        public IStorageConnectionConfiguration Use(string accountName, string password)
        {
            //if (String.IsNullOrWhiteSpace(accountName))
            //{
            //    throw new ArgumentException(nameof(accountName));
            //}
            if (AccountName != null)
            {
                throw new InvalidOperationException("An accountName has already been assigned to this StorageConnectionConfiguration");
            }
            //if (String.IsNullOrWhiteSpace(password))
            //{
            //    throw new ArgumentException(nameof(password));
            //}
            if (Password != null)
            {
                throw new InvalidOperationException("A password has already been assigned to this StorageConnectionConfiguration");
            }

            AccountName = accountName;
            Password = password;
            return this;
        }
    }
}