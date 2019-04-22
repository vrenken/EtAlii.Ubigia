namespace EtAlii.Ubigia.Api.Functional
{
    internal class RootContext : IRootContext
    {
        public IPathSubjectForOutputConverter Converter { get; }

        public IAddByNameToExistingPathProcessor AddByNameToExistingPathProcessor { get; }

        internal RootContext(
            IPathSubjectForOutputConverter converter,
            IAddByNameToExistingPathProcessor addByNameToExistingPathProcessor)
        {
            Converter = converter;
            AddByNameToExistingPathProcessor = addByNameToExistingPathProcessor;
        }
    }
}