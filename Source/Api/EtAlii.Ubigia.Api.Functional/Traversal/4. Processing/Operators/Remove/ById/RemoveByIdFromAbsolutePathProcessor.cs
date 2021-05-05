namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal class RemoveByIdFromAbsolutePathProcessor : IRemoveByIdFromAbsolutePathProcessor
    {
        public Task Process(OperatorParameters parameters)
        {
            return Task.FromException(new ScriptProcessingException("It is not possible to remove an existing node to the root of a space"));
        }
    }
}
