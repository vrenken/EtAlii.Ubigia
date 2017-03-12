namespace EtAlii.Ubigia.Api.Functional
{
    internal class AddByIdToAbsolutePathProcessor : IAddByIdToAbsolutePathProcessor
    {
        public void Process(OperatorParameters parameters)
        {
            throw new ScriptProcessingException("It is not possible to add an existing node to the root of a space");
        }
    }
}
