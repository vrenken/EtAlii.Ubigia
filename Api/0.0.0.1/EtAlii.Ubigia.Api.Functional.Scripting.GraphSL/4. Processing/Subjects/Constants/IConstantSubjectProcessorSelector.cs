namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IConstantSubjectProcessorSelector
    {
        IConstantSubjectProcessor Select(ConstantSubject subject);
    }
}