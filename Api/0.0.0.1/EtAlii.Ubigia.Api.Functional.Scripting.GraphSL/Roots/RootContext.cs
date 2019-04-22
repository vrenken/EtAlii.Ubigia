namespace EtAlii.Ubigia.Api.Functional
{
    internal class RootContext : IRootContext
    {
        public IPathSubjectForOutputConverter Converter { get; }

        public IAddAbsolutePathToExistingPathProcessor AddAbsolutePathToExistingPathProcessor { get; }

        internal RootContext(
            IPathSubjectForOutputConverter converter,
            IAddAbsolutePathToExistingPathProcessor addAbsolutePathToExistingPathProcessor)
        {
            Converter = converter;
            AddAbsolutePathToExistingPathProcessor = addAbsolutePathToExistingPathProcessor;
        }
    }
}