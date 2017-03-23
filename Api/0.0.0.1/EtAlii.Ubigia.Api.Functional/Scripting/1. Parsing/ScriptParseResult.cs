namespace EtAlii.Ubigia.Api.Functional
{
    public class ScriptParseResult
    {
        public string Source { get; }

        public Script Script { get; }

        public ScriptParserError[] Errors { get; }

        public ScriptParseResult(string source, Script script, ScriptParserError[] errors)
        {
            Source = source;
            Script = script;
            Errors = errors;
        }
    }
}
