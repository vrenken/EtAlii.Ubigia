namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IProcessingContext
    {
        IScriptScope Scope { get; }
        ILogicalContext Logical { get; }

        IPathSubjectToGraphPathConverter PathSubjectToGraphPathConverter { get; }
        IPathProcessor PathProcessor { get; }

        void Initialize(IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter, IPathProcessor pathProcessor);

    }
}