namespace EtAlii.Ubigia
{
    using System;

    /// <summary>
    /// A very unique identifier. 
    /// </summary>
    public partial struct Identifier : IEditableIdentifier
    {
        public Guid Storage => _storage;
        private Guid _storage;
        Guid IEditableIdentifier.Storage { get => _storage; set => _storage = value; }

        public Guid Account => _account;
        private Guid _account;
        Guid IEditableIdentifier.Account { get => _account; set => _account = value; }

        public Guid Space => _space;
        private Guid _space;
        Guid IEditableIdentifier.Space { get => _space; set => _space = value; }

        public ulong Era => _era;
        private ulong _era;
        ulong IEditableIdentifier.Era { get => _era; set => _era = value; }

        public ulong Period => _period;
        private ulong _period;
        ulong IEditableIdentifier.Period { get => _period; set => _period  = value; }

        public ulong Moment => _moment;
        private ulong _moment;
        ulong IEditableIdentifier.Moment { get => _moment; set => _moment = value; }
        
        public static readonly Identifier Empty = new Identifier
        {
            _storage = Guid.Empty,
            _account = Guid.Empty,
            _space = Guid.Empty,
            _era = ulong.MinValue,
            _period = ulong.MinValue,
            _moment = ulong.MinValue,
        };

        public override string ToString()
        {
            if(this == Empty)
            {
                return $"{GetType().Name}.Empty";
            }
            else
            {
                return string.Format("{0}{2}{1}", 
                                ToLocationString(), 
                                ToTimeString(),
                                IdentifierSplitter.Part);
            }
        }

        public string ToLocationString()
        {
            return string.Format("{0}{3}{1}{3}{2}",
                            _storage.ToString().Replace("-", ""),
                            _account.ToString().Replace("-", ""),
                            _space.ToString().Replace("-", ""),
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
