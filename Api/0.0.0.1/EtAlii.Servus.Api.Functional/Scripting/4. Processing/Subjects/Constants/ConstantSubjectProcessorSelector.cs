namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class ConstantSubjectProcessorSelector : Selector<ConstantSubject, IConstantSubjectProcessor>, IConstantSubjectProcessorSelector
    {
        public ConstantSubjectProcessorSelector(
            IStringConstantSubjectProcessor stringConstantSubjectProcessor,
            IObjectConstantSubjectProcessor objectConstantSubjectProcessor)
        {
            this.Register(subject => subject is StringConstantSubject, stringConstantSubjectProcessor)
                .Register(subject => subject is ObjectConstantSubject, objectConstantSubjectProcessor);
        }
    }
}
