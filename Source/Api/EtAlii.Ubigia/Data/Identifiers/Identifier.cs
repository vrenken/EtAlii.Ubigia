// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    /// <summary>
    /// A very unique identifier.
    /// The Ubigia identifier is not based on random values but on a hierarchical structure,
    /// composed of both spatial (Storage/Account/Space) and temporal (Era/Period/Moment) components.
    /// The reasoning is that we want to be able to uniquely identifier each individual change a person or system
    /// will ever needs.
    /// Each identifier is composed from both a spatial (Storage, Account, Space) and
    /// temporal component (Era, Period, Moment).
    /// </summary>
    public readonly partial struct Identifier
    {
        /// <summary>
        /// The storage to which the identifier belongs.
        /// <remarks>This does not explicitly indicate where the information is stored.</remarks>
        /// </summary>
        public Guid Storage { get; }

        /// <summary>
        /// The account to which the identifier belongs.
        /// </summary>
        public Guid Account { get; }

        /// <summary>
        /// The space to which the identifier belongs.
        /// </summary>
        public Guid Space { get; }

        /// <summary>
        ///
        /// </summary>
        public ulong Era { get; }

        public ulong Period { get; }

        public ulong Moment { get; }

        public static readonly Identifier Empty = new
        (
            storage: Guid.Empty,
            account: Guid.Empty,
            space: Guid.Empty,
            era: ulong.MinValue,
            period: ulong.MinValue,
            moment: ulong.MinValue
        );

        public override string ToString()
        {
            return this == Empty
                ? $"{GetType().Name}.Empty"
                : string.Format($"{ToLocationString()}{IdentifierSplitter.Part}{ToTimeString()}");
        }

        public string ToLocationString()
        {
            return string.Format("{0}{3}{1}{3}{2}",
                            Storage.ToString().Replace("-", ""),
                            Account.ToString().Replace("-", ""),
                            Space.ToString().Replace("-", ""),
                            IdentifierSplitter.Location);
        }

        public string ToTimeString()
        {
            return string.Format("{0}{3}{1}{3}{2}",
                            Era,
                            Period,
                            Moment,
                            IdentifierSplitter.Time);
        }

        public string ToDotSeparatedString()
        {
            return $"{Storage}.{Account}.{Space}.{Era}.{Period}.{Moment}";
        }
    }
}
