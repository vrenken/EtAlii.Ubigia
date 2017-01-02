namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.Servus.Api.Logical;

    public interface IScriptProcessorConfiguration
    {
        IScriptScope ScriptScope { get; }

        ILogicalContext LogicalContext { get; }

        IScriptProcessorExtension[] Extensions { get; }
        bool CachingEnabled { get; }

        IScriptProcessorConfiguration Use(IScriptScope scope);
        IScriptProcessorConfiguration Use(ILogicalContext logicalContext);

        IScriptProcessorConfiguration Use(IScriptProcessorExtension[] extensions);
        IScriptProcessorConfiguration UseCaching(bool cachingEnabled);
    }
}