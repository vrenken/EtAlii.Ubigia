namespace EtAlii.Ubigia.Api
{
    using System;

    public partial struct Identifier : IEquatable<Identifier>, IEditableIdentifier
    {
        public static Identifier NewIdentifier(Guid storage, Guid account, Guid space)
        {
            return new Identifier
            {
                _storage = storage,
                _account = account,
                _space = space,
                _era = UInt64.MinValue,
                _period = UInt64.MinValue,
                _moment = UInt64.MinValue,
            };
        }

        /*
         * The Factory methods below do not seem to be used.

        public static Identifier Create(Identifier id, UInt64 moment)
        {
            return new Identifier
            {
                _storage = id._storage,
                _account = id._account,
                _space = id._space,
                _era = id._era,
                _period = id._period,
                _moment = moment,
            };
        }

        public static Identifier Create(Identifier id, UInt64 period, UInt64 moment)
        {
            return new Identifier
            {
                _storage = id._storage,
                _account = id._account,
                _space = id._space,
                _era = id._era,
                _period = period,
                _moment = moment,
            };
        }

         */
        public static Identifier NewIdentifier(Identifier id, UInt64 era, UInt64 period, UInt64 moment)
        {
            return new Identifier
            {
                _storage = id._storage,
                _account = id._account,
                _space = id._space,
                _era = era,
                _period = period,
                _moment = moment,
            };
        }

        public static Identifier Create(Guid storage, Guid account, Guid space, UInt64 era, UInt64 period, UInt64 moment)
        {
            return new Identifier
            {
                _storage = storage,
                _account = account,
                _space = space,
                _era = era,
                _period = period,
                _moment = moment,
            };
        }
    }
}
