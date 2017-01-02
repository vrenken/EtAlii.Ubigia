//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.Servus.Api.Logical;
//    using EtAlii.xTechnology.MicroContainer;

//    internal class ProcessingContextScaffolding2 : IScaffolding
//    {
//        private readonly IScriptProcessorConfiguration _configuration;

//        public ProcessingContextScaffolding2(IScriptProcessorConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public void Register(Container container)
//        {
//            container.Register<IProcessingContext, ProcessingContext>();
//            container.Register<ILogicalContext>(() => _configuration.LogicalContext);
//            container.Register<IScriptScope>(() => _configuration.ScriptScope);
//        }
//    }
//}
