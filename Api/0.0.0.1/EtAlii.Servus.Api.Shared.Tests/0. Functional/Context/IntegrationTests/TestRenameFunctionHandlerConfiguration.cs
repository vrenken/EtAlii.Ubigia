namespace EtAlii.Servus.Api.Functional.Tests
{
    public class TestRenameFunctionHandlerConfiguration : IFunctionHandlerConfiguration
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public IFunctionHandler FunctionHandler { get { return _functionHandler; } }
        private readonly IFunctionHandler _functionHandler;

        public ArgumentSet[] ArgumentSets { get { return _argumentSets; } }
        private readonly ArgumentSet[] _argumentSets;

        public TestRenameFunctionHandlerConfiguration()
        {
            _name = "TestRename";
            _functionHandler = new TestRenameFunctionHandler();
            _argumentSets = new[]
            {
                new ArgumentSet(new[] {new Argument("source", typeof(string))}),
                new ArgumentSet(new[] {new Argument("source", typeof(string)), new Argument("destination", typeof(string))}),
            };
        }
    }
}
