namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using EtAlii.xTechnology.Structure;

    internal class ConstantSubjectProcessorSelector : Selector<ConstantSubject, IConstantSubjectProcessor>, IConstantSubjectProcessorSelector
    {
        public ConstantSubjectProcessorSelector(
            IStringConstantSubjectProcessor stringConstantSubjectProcessor,
            IObjectConstantSubjectProcessor objectConstantSubjectProcessor)
        {
            Register(subject => subject is StringConstantSubject, stringConstantSubjectProcessor)
                .Register(subject => subject is ObjectConstantSubject, objectConstantSubjectProcessor);
        }
    }
}
