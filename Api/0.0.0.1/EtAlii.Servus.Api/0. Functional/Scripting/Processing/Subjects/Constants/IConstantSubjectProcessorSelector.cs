namespace EtAlii.Servus.Api.Functional
{
    internal interface IConstantSubjectProcessorSelector
    {
        IConstantSubjectProcessor Select(ConstantSubject subject);
    }
}