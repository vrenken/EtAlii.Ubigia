namespace EtAlii.Servus.Api.Fabric
{
    using System.Threading.Tasks;

    public interface IEntryCacheChangeHandler
    {
        Task<IReadOnlyEntry> Handle(IEditableEntry entry, ExecutionScope scope);
    }
}