namespace EtAlii.Ubigia.Api.Functional
{
    // TODO: Remove this and replace it by the ProcessingContext.
    internal class RootContext : IRootContext
    {
        public IPathSubjectForOutputConverter Converter { get; }

        public IAddRelativePathToExistingPathProcessor AddRelativePathToExistingPathProcessor { get; }

        internal RootContext(
            IPathSubjectForOutputConverter converter, 
            IAddRelativePathToExistingPathProcessor addRelativePathToExistingPathProcessor)
        {
            Converter = converter;
            AddRelativePathToExistingPathProcessor = addRelativePathToExistingPathProcessor;
        }
    }
}