namespace EtAlii.Servus.Api.Functional
{
    internal class RemoveByIdFromAbsolutePathProcessor : IRemoveByIdFromAbsolutePathProcessor
    {
        public void Process(OperatorParameters parameters)
        {
            throw new ScriptProcessingException("It is not possible to remove an existing node to the root of a space");
        }
    }
}
