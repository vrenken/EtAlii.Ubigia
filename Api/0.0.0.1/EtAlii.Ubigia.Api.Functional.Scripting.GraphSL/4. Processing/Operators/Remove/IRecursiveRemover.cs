namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;

    internal interface IRecursiveRemover
    {
        Task<RecursiveRemoveResult> Remove(
            Identifier parentId,
            ConstantPathSubjectPart part,
            ExecutionScope scope);
    }
}