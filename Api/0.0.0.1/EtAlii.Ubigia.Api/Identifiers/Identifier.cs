namespace EtAlii.Ubigia.Api
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

        public UInt64 Era => _era;
        private UInt64 _era;

        public UInt64 Period => _period;
        private UInt64 _period;

        public UInt64 Moment => _moment;
        private UInt64 _moment;

        public static readonly Identifier Empty = new Identifier
        {
            _storage = Guid.Empty,
            _account = Guid.Empty,
            _space = Guid.Empty,
            _era = UInt64.MinValue,
            _period = UInt64.MinValue,
            _moment = UInt64.MinValue,
        };

        public override string ToString()
        {
            if(this == Empty)
            {
                return String.Format("{0}.Empty", this.GetType().Name);
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
                            _era,
                            _period,
                            _moment,
                            IdentifierSplitter.Time);
        }

        public string ToDotSeparatedString()
        {
            return String.Format("{0}.{1}.{2}.{3}.{4}.{5}", Storage, Account, Space, Era, Period, Moment);
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
                return _era;
            }
            set
            {
                _era = value;
            }
        }

        ulong IEditableIdentifier.Period
        {
            get
            {
                return _period;
            }
            set
            {
                _period  = value;
            }
        }

        ulong IEditableIdentifier.Moment
        {
            get
            {
                return _moment;
            }
            set
            {
                _moment = value;
            }
        }
    }
}
