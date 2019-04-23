namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IProcessingContext
    {
        IScriptScope Scope { get; }
        ILogicalContext Logical { get; }

        IPathSubjectToGraphPathConverter PathSubjectToGraphPathConverter { get; }
        IAbsolutePathSubjectProcessor AbsolutePathSubjectProcessor { get; }
        IRelativePathSubjectProcessor RelativePathSubjectProcessor { get; }
        IRootedPathSubjectProcessor RootedPathSubjectProcessor { get; }

        IPathProcessor PathProcessor { get; }

        void Initialize(
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IAbsolutePathSubjectProcessor absolutePathSubjectProcessor,
            IRelativePathSubjectProcessor relativePathSubjectProcessor,
            IRootedPathSubjectProcessor rootedPathSubjectProcessor,
            IPathProcessor pathProcessor);

    }
}