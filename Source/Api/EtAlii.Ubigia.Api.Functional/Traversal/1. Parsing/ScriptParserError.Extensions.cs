// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
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
