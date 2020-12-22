namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IScriptProcessingContext
    {
        IScriptScope Scope { get; }
        ILogicalContext Logical { get; }

        IPathSubjectToGraphPathConverter PathSubjectToGraphPathConverter { get; }
        IAbsolutePathSubjectProcessor AbsolutePathSubjectProcessor { get; }
        IRelativePathSubjectProcessor RelativePathSubjectProcessor { get; }
        IRootedPathSubjectProcessor RootedPathSubjectProcessor { get; }

        IPathSubjectForOutputConverter PathSubjectForOutputConverter { get; }
        IAddRelativePathToExistingPathProcessor AddRelativePathToExistingPathProcessor { get; }

        IPathProcessor PathProcessor { get; }

        void Initialize(
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IAbsolutePathSubjectProcessor absolutePathSubjectProcessor,
            IRelativePathSubjectProcessor relativePathSubjectProcessor,
            IRootedPathSubjectProcessor rootedPathSubjectProcessor,
            IPathProcessor pathProcessor,
            IPathSubjectForOutputConverter pathSubjectForOutputConverter,
            IAddRelativePathToExistingPathProcessor addRelativePathToExistingPathProcessor);

    }
}
