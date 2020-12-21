namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IItemToPathSubjectConverter
    {
        PathSubject Convert(object items);
        bool TryConvert(object items, out PathSubject pathSubject);
    }
}
