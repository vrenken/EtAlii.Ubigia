namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    internal class RemoveByNameFromAbsolutePathProcessor : IRemoveByNameFromAbsolutePathProcessor
    {
        public Task Process(OperatorParameters parameters)
        {
            throw new ScriptProcessingException("It is not possible to remove an absolute defined node from a space");
        }
    }
}
