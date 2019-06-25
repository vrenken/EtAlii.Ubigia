﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System.Text;
    using Moppet.Lapa;

    internal static class LpDebuggerExtensions
    {
        private static int _indention;
        public static LpsParser Debug(this LpsParser parser, string debugId, bool showDetails = false)
        {
            return new LpsParser(text =>
            {
                var sb = new StringBuilder();
                for (int i = 0; i < _indention; i++)
                {
                    sb.Append("\t");
                }
                var spaces = sb.ToString();

                System.Diagnostics.Debug.WriteLine($"{spaces}+ {debugId} - Input: \"{Escape(text.ToString())}\"");
                _indention += 1;
                var result = parser.Do(text);
                _indention -= 1;
                var successText = result.Success ? "Success" : "Failure";
                var detailsText = showDetails ? ":" : "";
                System.Diagnostics.Debug.WriteLine($"{spaces}- {debugId} - {successText}{detailsText}");
                if (showDetails)
                {
                    if (result.Success)
                    {
                        var resultText = result.ToString();
                        resultText = Escape(resultText);//, spaces);

                        resultText = resultText == string.Empty ? "string.Empty" : resultText;
                        System.Diagnostics.Debug.WriteLine($"{spaces}  > Match: \"{resultText}\"");
                    }
                    var restText = result.Rest.ToString();
                    restText = Escape(restText);//, spaces);
                    System.Diagnostics.Debug.WriteLine($"{spaces}  > Rest: \"{restText}\"");
                }
                return result;
            });
        }

        private static string Escape(string s)//, string spaces)
        {
            return s.Replace("\n", "\\n")
                    .Replace("\r", "\\r")
                    .Replace("\\r\\n", "\\n");
                    //.Replace("\\n", $"\\n{spaces}");
        }
    }
}
