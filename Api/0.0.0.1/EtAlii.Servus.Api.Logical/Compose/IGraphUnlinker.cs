namespace EtAlii.Servus.Api.Logical
{
    using System.Threading.Tasks;

    public interface IGraphUnlinker
    {
        Task<IReadOnlyEntry> Unlink(Identifier location, string itemName, Identifier item, ExecutionScope scope);
    }
}