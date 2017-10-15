namespace EtAlii.Servus.Model.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial struct Identifier
    {
        public UInt64 Root { get { return _root; } }
        private UInt64 _root;

        public string Account { get { return _account; } }
        private string _account;

        public UInt64 Period { get { return _period; } }
        private UInt64 _period;

        public UInt64 Moment { get { return _moment; } }
        private UInt64 _moment;

        public static readonly Identifier Empty = new Identifier
        {
            _root = UInt64.MinValue,
            _account = String.Empty,
            _period = UInt64.MinValue,
            _moment = UInt64.MinValue,
        };
    }
}
