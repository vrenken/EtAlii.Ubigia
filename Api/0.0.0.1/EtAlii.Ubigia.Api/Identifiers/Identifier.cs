﻿namespace EtAlii.Ubigia.Api
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// A very unique identifier. 
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public partial struct Identifier : IEditableIdentifier
    {
        public Guid Storage => _storage;
        private Guid _storage;

        public Guid Account => _account;
        private Guid _account;

        public Guid Space => _space;
        private Guid _space;

        public UInt64 Era { get; private set; }

        public UInt64 Period { get; private set; }

        public UInt64 Moment { get; private set; }

        public static readonly Identifier Empty = new Identifier
        {
            _storage = Guid.Empty,
            _account = Guid.Empty,
            _space = Guid.Empty,
            Era = UInt64.MinValue,
            Period = UInt64.MinValue,
            Moment = UInt64.MinValue,
        };

        public override string ToString()
        {
            if(this == Empty)
            {
                return $"{GetType().Name}.Empty";
            }
            else
            {
                return String.Format("{0}{2}{1}", 
                                ToLocationString(), 
                                ToTimeString(),
                                IdentifierSplitter.Part);
            }
        }

        public string ToLocationString()
        {
            return String.Format("{0}{3}{1}{3}{2}",
                            _storage.ToString().Replace("-", ""),
                            _account.ToString().Replace("-", ""),
                            _space.ToString().Replace("-", ""),
                            IdentifierSplitter.Location);
        }

        public string ToTimeString()
        {
            return String.Format("{0}{3}{1}{3}{2}",
                            Era,
                            Period,
                            Moment,
                            IdentifierSplitter.Time);
        }

        public string ToDotSeparatedString()
        {
            return $"{Storage}.{Account}.{Space}.{Era}.{Period}.{Moment}";
        }

        Guid IEditableIdentifier.Storage
        {
            get
            {
                return _storage;
            }
            set
            {
                _storage = value;
            }
        }

        Guid IEditableIdentifier.Account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
            }
        }

        Guid IEditableIdentifier.Space
        {
            get
            {
                return _space;
            }
            set
            {
                _space = value;
            }
        }

        ulong IEditableIdentifier.Era
        {
            get
            {
                return Era;
            }
            set
            {
                Era = value;
            }
        }

        ulong IEditableIdentifier.Period
        {
            get
            {
                return Period;
            }
            set
            {
                Period  = value;
            }
        }

        ulong IEditableIdentifier.Moment
        {
            get
            {
                return Moment;
            }
            set
            {
                Moment = value;
            }
        }
    }
}
