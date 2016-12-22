namespace EtAlii.Servus.Api.Logical
{
    using System.Threading.Tasks;

    public interface IGraphAdder
    {
        Task<IReadOnlyEntry> Add(Identifier parent, string child, ExecutionScope scope);
        Task<IReadOnlyEntry> Add(Identifier parent, Identifier child, ExecutionScope scope);
    }
}