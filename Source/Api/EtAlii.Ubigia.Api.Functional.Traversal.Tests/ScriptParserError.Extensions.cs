namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Text;

    public static class ScriptParserErrorExtensions
    {
        public static string DumpAsString(this ScriptParserError[] errors)
        {
            var sb = new StringBuilder();

            foreach (var error in errors)
            {
                sb.Append(error.Message);
                sb.AppendLine();
                sb.Append(error.Exception.Message);
                sb.AppendLine();
                sb.Append(error.Exception.StackTrace);
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
