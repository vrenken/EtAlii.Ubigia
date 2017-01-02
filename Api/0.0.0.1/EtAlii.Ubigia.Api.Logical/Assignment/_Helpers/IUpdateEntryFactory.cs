namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    internal interface IUpdateEntryFactory
    {
        Task<IEditableEntry> Create(
            IReadOnlyEntry entry,
            ExecutionScope scope);
    }
}