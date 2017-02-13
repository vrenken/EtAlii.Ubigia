using System.Collections.Generic;

namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    internal interface IStringEscaper
    {
        string Escape(string item);
        IEnumerable<string> Escape(IEnumerable<string> items);
    }
}