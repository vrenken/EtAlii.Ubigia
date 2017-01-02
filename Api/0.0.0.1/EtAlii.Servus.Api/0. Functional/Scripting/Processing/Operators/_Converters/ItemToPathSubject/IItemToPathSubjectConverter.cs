namespace EtAlii.Servus.Api.Functional
{
    internal interface IItemToPathSubjectConverter
    {
        PathSubject Convert(object items);
        bool TryConvert(object items, out PathSubject pathSubject);
    }
}