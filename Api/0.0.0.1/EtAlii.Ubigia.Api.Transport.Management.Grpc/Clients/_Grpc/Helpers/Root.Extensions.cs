namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class RootExtension
    {
        public static Root ToLocal(this WireProtocol.Root root)
        {
            return new Root
            {
                Id = root.Id.ToLocal(),
                Identifier = root.Identifier.ToLocal(),
                Name = root.Name
            };
        }

        public static WireProtocol.Root ToWire(this Root root)
        {
            return new WireProtocol.Root
            {
                Id = root.Id.ToWire(),
                Identifier = root.Identifier.ToWire(),
                Name = root.Name
            };
        }

        public static IEnumerable<WireProtocol.Root> ToWire(this IEnumerable<Root> roots)
        {
            return roots.Select(s => s.ToWire());
        }
    }
}
