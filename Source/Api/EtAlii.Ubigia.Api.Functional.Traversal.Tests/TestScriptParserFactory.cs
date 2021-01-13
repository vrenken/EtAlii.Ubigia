// This file seems not in the right place. Should be moved to somewhere better.
// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class TestScriptParserFactory : ScriptParserFactory

    {
        public IScriptParser Create()
        {
            return Create(new TestTraversalParserConfiguration());
        }
    }
}
