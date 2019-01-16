namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;

    public class StorageConnectionConfiguration : IStorageConnectionConfiguration
    {
        public IStorageTransport Transport { get; private set; }

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
    }
}