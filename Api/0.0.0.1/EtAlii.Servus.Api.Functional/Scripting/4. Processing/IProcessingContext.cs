namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.Servus.Api.Logical;

    public interface IProcessingContext
    {
        IScriptScope Scope { get; }
        ILogicalContext Logical { get; }

        IPathSubjectToGraphPathConverter PathSubjectToGraphPathConverter { get; }
        IPathProcessor PathProcessor { get; }

        void Initialize(IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter, IPathProcessor pathProcessor);

    }
}