namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class StorageExtension
    {
        public static Storage ToLocal(this WireProtocol.Storage storage)
        {
            return new Storage
            {
                Id = storage.Id.ToLocal(),
                Address = storage.Address,
                Name = storage.Name
            };
        }

        public static WireProtocol.Storage ToWire(this Storage storage)
        {
            return new WireProtocol.Storage
            {
                Id = storage.Id.ToWire(),
                Address = storage.Address,
                Name = storage.Name
            };
        }

        public static IEnumerable<WireProtocol.Storage> ToWire(this IEnumerable<Storage> storages)
        {
            return storages.Select(s => s.ToWire());
        }
        public static IEnumerable<Storage> ToLocal(this IEnumerable<WireProtocol.Storage> storages)
        {
            return storages.Select(s => s.ToLocal());
        }
    }
}
