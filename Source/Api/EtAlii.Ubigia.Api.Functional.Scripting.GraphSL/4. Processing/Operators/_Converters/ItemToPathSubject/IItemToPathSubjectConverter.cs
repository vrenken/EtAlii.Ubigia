namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IItemToPathSubjectConverter
    {
        PathSubject Convert(object items);
        bool TryConvert(object items, out PathSubject pathSubject);
    }
}