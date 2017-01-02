namespace EtAlii.Servus.Api.Functional
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptParsingScaffolding : IScaffolding
    {
        private readonly IFunctionHandlerConfiguration[] _functionHandlerConfigurations;

        public ScriptParsingScaffolding(IFunctionHandlerConfiguration[] functionHandlerConfigurations)
        {
            _functionHandlerConfigurations = functionHandlerConfigurations;
        }

        public void Register(Container container)
        {
            container.Register<IScriptParserFactory, ScriptParserFactory>(Lifestyle.Singleton);
            container.Register<IScriptParser>(() => container.GetInstance<IScriptParserFactory>().Create(), Lifestyle.Singleton);
        }
    }
}
