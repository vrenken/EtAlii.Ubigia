namespace EtAlii.Servus.Api.Data
{
    internal interface IAddItemHelper
    {
        DynamicNode AddNewEntry(AddItem action, IReadOnlyEntry entry);
    }
}