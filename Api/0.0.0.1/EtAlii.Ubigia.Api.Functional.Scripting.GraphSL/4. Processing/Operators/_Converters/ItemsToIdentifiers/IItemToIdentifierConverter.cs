namespace EtAlii.Ubigia.Api.Functional
{
    public interface IItemToIdentifierConverter
    {
        Identifier Convert(object item);
    }
}