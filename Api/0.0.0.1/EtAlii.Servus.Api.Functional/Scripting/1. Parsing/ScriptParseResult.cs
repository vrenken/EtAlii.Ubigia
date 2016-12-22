namespace EtAlii.Servus.Api.Functional
{
    public class ScriptParseResult
    {
        public string Source { get {return _source; } }
        private readonly string _source;

        public Script Script { get { return _script; } }
        private readonly Script _script;

        public ScriptParserError[] Errors { get { return _errors; } }
        private readonly ScriptParserError[] _errors;

        public ScriptParseResult(string source, Script script, ScriptParserError[] errors)
        {
            _source = source;
            _script = script;
            _errors = errors;
        }
    }
}
