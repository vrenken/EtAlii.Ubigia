namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingDataContextExtension : IDataContextExtension
    {
        public void Initialize(Container container)
        {
            container.RegisterDecorator(typeof(IDataContext), typeof(ProfilingDataContext));
//            container.RegisterDecorator(typeof(IGraphSLScriptContext), typeof(ProfilingGraphSLScriptContext));
//
//            container.RegisterDecorator(typeof(IScriptProcessorFactory), typeof(ProfilingScriptProcessorFactory));
//            container.RegisterDecorator(typeof(IScriptParserFactory), typeof(ProfilingScriptParserFactory));

            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.Context));
        }
    }
}