// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class RootExtension
    {
        public static Root ToLocal(this WireProtocol.Root root)
        {
            return new()
            {
                Id = root.Id.ToLocal(),
                Name = root.Name,
                Type = root.Type == string.Empty ? RootType.None : new RootType(root.Type), // RT2022: We cannot change the root type yet.
                Identifier = root.Identifier.ToLocal(),
            };
        }

        public static IEnumerable<Root> ToLocal(this IEnumerable<WireProtocol.Root> roots)
        {
            return roots.Select(s => s.ToLocal());
        }

        public static WireProtocol.Root ToWire(this Root root)
        {
            return new()
            {
                Id = root.Id.ToWire(),
                Name = root.Name,
                Type = root.Type.Value ?? string.Empty, // RT2022: We cannot change the root type yet.
                Identifier = root.Identifier.ToWire(),
            };
        }

        public static IEnumerable<WireProtocol.Root> ToWire(this IEnumerable<Root> roots)
        {
            return roots.Select(s => s.ToWire());
        }
    }
}
