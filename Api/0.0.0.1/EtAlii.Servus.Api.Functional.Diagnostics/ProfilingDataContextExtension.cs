namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using EtAlii.Servus.Api.Functional;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingDataContextExtension : IDataContextExtension
    {
        public void Initialize(Container container)
        {
            container.RegisterDecorator(typeof(IDataContext), typeof(ProfilingDataContext));
            container.RegisterDecorator(typeof(IScriptsSet), typeof(ProfilingScriptSet));

            container.RegisterDecorator(typeof(IScriptProcessorFactory), typeof(ProfilingScriptProcessorFactory));
            container.RegisterDecorator(typeof(IScriptParserFactory), typeof(ProfilingScriptParserFactory));

            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.Context));
        }
    }
}