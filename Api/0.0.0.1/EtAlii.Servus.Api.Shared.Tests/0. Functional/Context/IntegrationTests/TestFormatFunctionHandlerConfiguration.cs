namespace EtAlii.Servus.Api.Functional.Tests
{
    public class TestFormatFunctionHandlerConfiguration : IFunctionHandlerConfiguration
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public IFunctionHandler FunctionHandler { get { return _functionHandler; } }
        private readonly IFunctionHandler _functionHandler;

        public ArgumentSet[] ArgumentSets { get { return _argumentSets; } }
        private readonly ArgumentSet[] _argumentSets;

        public TestFormatFunctionHandlerConfiguration()
        {
            _name = "TestFormat";
            _functionHandler = new TestFormatFunctionHandler();
            _argumentSets = new[]
            {
                new ArgumentSet(new[] {new Argument("value1", typeof(string))}),
                new ArgumentSet(new[] {new Argument("value1", typeof(string)), new Argument("value2", typeof(string))}),
            };
        }
    }
}
