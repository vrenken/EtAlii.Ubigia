namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IConstantSubjectProcessorSelector
    {
        IConstantSubjectProcessor Select(ConstantSubject subject);
    }
}
