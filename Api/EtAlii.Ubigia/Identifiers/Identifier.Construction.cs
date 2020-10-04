namespace EtAlii.Ubigia
{
    using System;

    public partial struct Identifier
    {
        public static Identifier NewIdentifier(Guid storage, Guid account, Guid space)
        {
            return new Identifier
            {
                _storage = storage,
                _account = account,
                _space = space,
                Era = ulong.MinValue,
                Period = ulong.MinValue,
                Moment = ulong.MinValue,
            };
        }

        public static Identifier NewIdentifier(Identifier id, ulong era, ulong period, ulong moment)
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

        public static Identifier Create(Guid storage, Guid account, Guid space, ulong era, ulong period, ulong moment)
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
