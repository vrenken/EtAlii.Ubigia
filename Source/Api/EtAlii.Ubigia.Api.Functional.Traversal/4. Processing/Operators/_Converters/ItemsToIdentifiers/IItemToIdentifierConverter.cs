namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public interface IItemToIdentifierConverter
    {
        Identifier Convert(object item);
    }
}
