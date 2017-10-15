namespace EtAlii.Servus.Infrastructure.Model
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Stream
    {
        public Guid Storage { get { return _storage; } }
        private Guid _storage;

        public Guid Account { get { return _account; } }
        private Guid _account;

        public Guid Space { get { return _space; } }
        private Guid _space;

        public HashSet<Entry> Entries { get { return _entries; } }
        private HashSet<Entry> _entries;

        private Stream()
        {
        }

        public static Stream Create(Guid storage, Guid account, Guid space)
        {
            return new Stream
            {
                _storage = storage,
                _account = account,
                _space = space,
                _entries = new HashSet<Entry>(),
            };
        }

    }
}
