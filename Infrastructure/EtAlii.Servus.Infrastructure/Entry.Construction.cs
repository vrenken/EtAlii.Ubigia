namespace EtAlii.Servus.Model.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class Entry
    {
        public static Entry CreateOrigin(UInt64 root, string account)
        {
            return new Entry
            {
                _id = Identifier.Create(root, account),
                _past = Identifier.Empty,
                _future = new List<Identifier>(),
            };
        }

        public static Entry Create(Entry past)
        {
            return new Entry
            {
                _id = Identifier.Create(past.Id.Root, past.Id.Account),
                _past = past.Id,
                _future = new List<Identifier>(),
            };
        }
    }
}
