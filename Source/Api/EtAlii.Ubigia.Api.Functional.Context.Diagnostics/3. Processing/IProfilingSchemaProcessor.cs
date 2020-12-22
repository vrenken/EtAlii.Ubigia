namespace EtAlii.Ubigia.Api.Functional.Context.Diagnostics
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal interface IProfilingSchemaProcessor : ISchemaProcessor
    {
        IProfiler Profiler { get; }
    }
}
