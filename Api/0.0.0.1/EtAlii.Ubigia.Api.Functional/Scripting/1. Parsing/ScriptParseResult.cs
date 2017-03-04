namespace EtAlii.Ubigia.Api.Functional
{
    public class ScriptParseResult
    {
        public string Source => _source;
        private readonly string _source;

        public Script Script => _script;
        private readonly Script _script;

        public ScriptParserError[] Errors => _errors;
        private readonly ScriptParserError[] _errors;

        public ScriptParseResult(string source, Script script, ScriptParserError[] errors)
        {
            _source = source;
            _script = script;
            _errors = errors;
        }
    }
}
