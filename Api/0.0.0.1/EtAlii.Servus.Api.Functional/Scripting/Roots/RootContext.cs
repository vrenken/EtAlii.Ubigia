namespace EtAlii.Servus.Api.Functional
{
    internal class RootContext : IRootContext
    {
        public IPathSubjectForOutputConverter Converter { get; }

        public IAddByNameToRelativePathProcessor AddByNameToRelativePathProcessor { get; }

        internal RootContext(
            IPathSubjectForOutputConverter converter,
            IAddByNameToRelativePathProcessor addByNameToRelativePathProcessor)
        {
            Converter = converter;
            AddByNameToRelativePathProcessor = addByNameToRelativePathProcessor;
        }
    }
}