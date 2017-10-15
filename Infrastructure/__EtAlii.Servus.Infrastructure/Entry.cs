namespace EtAlii.Servus.Model.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class Entry
    {
        public Identifier Id { get { return _id; } }
        private Identifier _id;

        public Identifier Past { get { return _past; } }
        private Identifier _past;

        public List<Identifier> Future { get { return _future; } }
        private List<Identifier> _future;
    }
}
