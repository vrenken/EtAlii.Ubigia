namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    public interface IItemToIdentifierConverter
    {
        Identifier Convert(object item);
    }
}