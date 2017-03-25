namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class StringEscaper : IStringEscaper
    {
        public string Escape(string item)
        {
            return $"\"{item}\"";
        }

        public IEnumerable<string> Escape(IEnumerable<string> items)
        {
            return items.Select(Escape);
        }

    }
}
