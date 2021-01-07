namespace EtAlii.Ubigia
{
    using System;

    public readonly partial struct Identifier
    {
#pragma warning disable S1144 // SonarQube does not seem to understand the new C#9 new() constructor.
        private Identifier(Guid storage, Guid account, Guid space, ulong era, ulong period, ulong moment)
        {
            Storage = storage;
            Account = account;
            Space = space;
            Era = era;
            Period = period;
            Moment = moment;
        }
#pragma warning restore S1144

        public static Identifier NewIdentifier(Guid storage, Guid account, Guid space)
        {
            return new(
                storage: storage,
                account: account,
                space: space,
                era: ulong.MinValue,
                period: ulong.MinValue,
                moment: ulong.MinValue
            );
        }

        public static Identifier NewIdentifier(in Identifier id, ulong era, ulong period, ulong moment)
        {
            return new(
                storage: id.Storage,
                account: id.Account,
                space: id.Space,
                era: era,
                period: period,
                moment: moment
            );
        }

        public static Identifier Create(Guid storage, Guid account, Guid space, ulong era, ulong period, ulong moment)
        {
            return new(
                storage: storage,
                account: account,
                space: space,
                era: era,
                period: period,
                moment: moment
            );
        }
    }
}
