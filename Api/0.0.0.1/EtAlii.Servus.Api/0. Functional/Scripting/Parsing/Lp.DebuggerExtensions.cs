namespace EtAlii.Servus.Api.Functional
{
    using System;
    using Moppet.Lapa;

    internal static class LpDebuggerExtensions
    {
        public static LpsParser Debug(this LpsParser parser, string debugId, bool showDetails = false)
        {
            return new LpsParser(text =>
            {
                var result = parser.Do(text);
                System.Diagnostics.Debug.WriteLine("{0} = {1}{2}", debugId, result.Success ? "Success" : "Failure", showDetails ? ":" : "");
                if (showDetails)
                {
                    if (result.Success)
                    {
                        var resultText = result.ToString();
                        resultText = Escape(resultText);

                        resultText = resultText == String.Empty ? "String.Empty" : resultText;
                        System.Diagnostics.Debug.WriteLine("- Match: {0}", resultText);
                    }
                    var restText = result.Rest.ToString();
                    restText = Escape(restText);
                    System.Diagnostics.Debug.WriteLine("- Rest: {0}", restText);
                }
                return result;
            });
        }

        private static string Escape(string s)
        {
            return s.Replace("\n", "\\n")
                    .Replace("\r", "\\r");
        }
    }
}
