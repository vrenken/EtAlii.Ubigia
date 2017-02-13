namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class LocalPathSplitter : ILocalPathSplitter
    {
        public void Split(string localStart, string path, out string last, out string[] rest)
        {
            var localStartParts = localStart.Split('\\');
            var localParts = path.Split('\\');

            var localRelativeParts = localParts
                .Skip(localStartParts.Count())
                .ToArray();

            var relativePartCount = localRelativeParts.Count();

            last = localRelativeParts.Last();
            rest = localRelativeParts
                .Take(relativePartCount - 1)
                .ToArray();
        }
    }
}
