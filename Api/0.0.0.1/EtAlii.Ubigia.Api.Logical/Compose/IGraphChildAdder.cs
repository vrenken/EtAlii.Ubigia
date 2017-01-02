namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IGraphChildAdder
    {
        Task<IReadOnlyEntry> TryAddChild(Identifier location, ExecutionScope scope);
        Task<IReadOnlyEntry> AddChild(Identifier location, string name, ExecutionScope scope);
        Task<IReadOnlyEntry> AddChild(Identifier location, Identifier child, ExecutionScope scope);
    }
}