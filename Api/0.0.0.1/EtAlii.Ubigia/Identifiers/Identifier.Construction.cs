namespace EtAlii.Ubigia
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
                Era = UInt64.MinValue,
                Period = UInt64.MinValue,
                Moment = UInt64.MinValue,
            };
        }

        public static Identifier NewIdentifier(Identifier id, UInt64 era, UInt64 period, UInt64 moment)
        {
            return new Identifier
            {
                _storage = id._storage,
                _account = id._account,
                _space = id._space,
                Era = era,
                Period = period,
                Moment = moment,
            };
        }

        public static Identifier Create(Guid storage, Guid account, Guid space, UInt64 era, UInt64 period, UInt64 moment)
        {
            return new Identifier
            {
                _storage = storage,
                _account = account,
                _space = space,
                Era = era,
                Period = period,
                Moment = moment,
            };
        }
    }
}
