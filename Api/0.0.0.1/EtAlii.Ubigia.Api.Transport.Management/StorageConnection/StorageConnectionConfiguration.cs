namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;

    public class StorageConnectionConfiguration : Configuration<StorageConnectionConfiguration>, IStorageConnectionConfiguration
    {
        public IStorageTransport Transport { get; private set; }

        public new IStorageConnectionConfiguration Use(IStorageConnectionExtension[] extensions)
        {
            return base.Use(extensions);
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