namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class StringEscaper
    {
        public string Escape(string item)
        {
            return String.Format("\"{0}\"", item);
        }

        public IEnumerable<string> Escape(IEnumerable<string> items)
        {
            return items.Select(Escape);
        }

    }
}
