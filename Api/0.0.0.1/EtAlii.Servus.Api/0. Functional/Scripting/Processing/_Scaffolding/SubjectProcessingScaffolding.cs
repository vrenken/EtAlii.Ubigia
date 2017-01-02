namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class SubjectProcessingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IPathSubjectProcessor, PathSubjectProcessor>();
            container.Register<IConstantSubjectsProcessor, ConstantSubjectsProcessor>();
            container.Register<IVariableSubjectProcessor, VariableSubjectProcessor>();
            container.Register<IPathSubjectForOutputConverter, PathSubjectForOutputConverter>();
            container.Register<IStringConstantSubjectProcessor, StringConstantSubjectProcessor>();
            container.Register<IObjectConstantSubjectProcessor, ObjectConstantSubjectProcessor>();
            container.Register<IConstantSubjectProcessorSelector, ConstantSubjectProcessorSelector>();
        }
    }
}
