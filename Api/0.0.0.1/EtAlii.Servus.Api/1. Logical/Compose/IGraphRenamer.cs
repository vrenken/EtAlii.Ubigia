namespace EtAlii.Servus.Api.Logical
{
    using System.Threading.Tasks;

    public interface IGraphRenamer
    {
        Task<IReadOnlyEntry> Rename(Identifier item, string newName, ExecutionScope scope);
    }
}