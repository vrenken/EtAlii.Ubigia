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

        public UInt64 Era { get; private set; }
        ulong IEditableIdentifier.Era { get => Era; set => Era = value; }

        public UInt64 Period { get; private set; }
        ulong IEditableIdentifier.Period { get => Period; set => Period  = value; }

        public UInt64 Moment { get; private set; }
        ulong IEditableIdentifier.Moment { get => Moment; set => Moment = value; }
        
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
    }
}
