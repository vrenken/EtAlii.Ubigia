namespace EtAlii.Servus.Api.Functional
{
    internal class RootContext : IRootContext
    {
        public IPathSubjectForOutputConverter Converter { get; }

        public IAddByNameToRelativePathProcessor AddByNameToRelativePathProcessor { get; }

        public IAbsolutePathSubjectProcessor AbsolutePathSubjectProcessor { get; }

        internal RootContext(
            IPathSubjectForOutputConverter converter,
            IAddByNameToRelativePathProcessor addByNameToRelativePathProcessor,
            IAbsolutePathSubjectProcessor absolutePathSubjectProcessor)
        {
            Converter = converter;
            AddByNameToRelativePathProcessor = addByNameToRelativePathProcessor;
            AbsolutePathSubjectProcessor = absolutePathSubjectProcessor;
        }
    }
}