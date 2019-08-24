namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingGraphSLScriptContextExtension : IGraphSLScriptContextExtension
    {
        public void Initialize(Container container)
        {
            container.RegisterDecorator(typeof(IGraphSLScriptContext), typeof(ProfilingGraphSLScriptContext));

            container.RegisterDecorator(typeof(IScriptProcessorFactory), typeof(ProfilingScriptProcessorFactory));
            container.RegisterDecorator(typeof(IScriptParserFactory), typeof(ProfilingScriptParserFactory));

            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.Context));
        }
    }
}