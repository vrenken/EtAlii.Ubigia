namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class StorageExtension
    {
        public static Storage ToLocal(this WireProtocol.Storage storage)
        {
            return new()
            {
                Id = storage.Id.ToLocal(),
                Address = storage.Address,
                Name = storage.Name
            };
        }

        public static WireProtocol.Storage ToWire(this Storage storage)
        {
            return new()
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
    }
}
