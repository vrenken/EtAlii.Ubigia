// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class SpaceExtension
    {
        public static Space ToLocal(this WireProtocol.Space space)
        {
            return space == null ? null : new Space
            {
                Id = space.Id.ToLocal(),
                AccountId = space.AccountId.ToLocal(),
                Name = space.Name
            };
        }

        public static WireProtocol.Space ToWire(this Space space)
        {
            return space == null ? null : new WireProtocol.Space
            {
                Id = space.Id.ToWire(),
                AccountId = space.AccountId.ToWire(),
                Name = space.Name
            };
        }

        public static IEnumerable<WireProtocol.Space> ToWire(this IEnumerable<Space> spaces)
        {
            return spaces.Select(s => s.ToWire());
        }
    }
}
