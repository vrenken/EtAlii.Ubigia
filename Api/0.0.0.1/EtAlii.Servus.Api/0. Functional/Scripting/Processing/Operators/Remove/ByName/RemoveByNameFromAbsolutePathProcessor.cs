namespace EtAlii.Servus.Api.Functional
{
    internal class RemoveByNameFromAbsolutePathProcessor : IRemoveByNameFromAbsolutePathProcessor
    {
        public void Process(OperatorParameters parameters)
        {
            throw new ScriptProcessingException("It is not possible to remove an absolute defined node from a space");
        }
    }
}
