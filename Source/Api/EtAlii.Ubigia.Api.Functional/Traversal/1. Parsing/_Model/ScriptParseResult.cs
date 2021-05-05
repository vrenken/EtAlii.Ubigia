namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public class ScriptParseResult
    {
        /// <summary>
        /// The source that got parsed.
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// The script that got created from the provided source.
        /// </summary>
        public Script Script { get; }

        /// <summary>
        /// If any, this property shows any parsing errors.
        /// </summary>
        public ScriptParserError[] Errors { get; }

        internal ScriptParseResult(string source, Script script, ScriptParserError[] errors)
        {
            Source = source;
            Script = script;
            Errors = errors;
        }
    }
}
