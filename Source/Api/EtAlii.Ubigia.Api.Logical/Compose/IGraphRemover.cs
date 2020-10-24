namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IGraphRemover
    {
        Task<IReadOnlyEntry> Remove(Identifier parent, string child, ExecutionScope scope);
        Task<IReadOnlyEntry> Remove(Identifier parent, Identifier child, ExecutionScope scope);
    }
}