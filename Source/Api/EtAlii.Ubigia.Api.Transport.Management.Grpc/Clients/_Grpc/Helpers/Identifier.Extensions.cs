// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class IdentifierExtension 
    {
        public static Identifier ToLocal(this WireProtocol.Identifier id)
        {
            var storage = id.Storage.ToLocal();
            var account = id.Account.ToLocal();
            var space = id.Space.ToLocal();

            var era = id.Era;
            var period = id.Period;
            var moment = id.Moment;
            
            return Identifier.Create(storage,account, space, era, period, moment);
        }
        
        public static IEnumerable<Identifier> ToLocal(this IEnumerable<WireProtocol.Identifier> identifiers)
        {
            return identifiers.Select(id => id.ToLocal());
        }
        
        public static WireProtocol.Identifier ToWire(this Identifier identifier)
        {
            var storage = identifier.Storage.ToWire();
            var account = identifier.Account.ToWire();
            var space = identifier.Space.ToWire();

            var era = identifier.Era;
            var period = identifier.Period;
            var moment = identifier.Moment;
            
            return new WireProtocol.Identifier
            {
                Storage = storage,
                Account = account, 
                Space = space, 
                Era = era,
                Period = period, 
                Moment = moment,
            };
        }
        
        public static IEnumerable<WireProtocol.Identifier> ToWire(this IEnumerable<Identifier> identifiers)
        {
            return identifiers.Select(id => id.ToWire());
        }
    }
}
