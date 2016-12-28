namespace EtAlii.Servus.Api.Functional
{
    internal class RootContext : IRootContext
    {
        public IPathSubjectForOutputConverter Converter { get; }

        public IAddByNameToRelativePathProcessor AddByNameToRelativePathProcessor { get; }

        public INonRootedPathSubjectProcessor NonRootedPathSubjectProcessor { get; }

        internal RootContext(
            IPathSubjectForOutputConverter converter,
            IAddByNameToRelativePathProcessor addByNameToRelativePathProcessor, 
            INonRootedPathSubjectProcessor nonRootedPathSubjectProcessor)
        {
            Converter = converter;
            AddByNameToRelativePathProcessor = addByNameToRelativePathProcessor;
            NonRootedPathSubjectProcessor = nonRootedPathSubjectProcessor;
        }
    }
}