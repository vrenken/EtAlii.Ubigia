namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IConstantSubjectProcessorSelector
    {
        IConstantSubjectProcessor Select(ConstantSubject subject);
    }
}