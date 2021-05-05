namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal interface IRecursiveAdder
    {
        Task<RecursiveAddResult> Add(Identifier parentId, ConstantPathSubjectPart part, IEditableEntry newEntry, ExecutionScope scope);
    }
}
