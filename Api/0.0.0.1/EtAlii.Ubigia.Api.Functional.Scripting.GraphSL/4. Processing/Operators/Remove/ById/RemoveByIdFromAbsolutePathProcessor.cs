namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    internal class RemoveByIdFromAbsolutePathProcessor : IRemoveByIdFromAbsolutePathProcessor
    {
        public Task Process(OperatorParameters parameters)
        {
            throw new ScriptProcessingException("It is not possible to remove an existing node to the root of a space");
        }
    }
}
