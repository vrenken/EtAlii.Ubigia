namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    internal class AddByIdAsNewPathProcessor : IAddByIdAsNewPathProcessor
    {
        public Task Process(OperatorParameters parameters)
        {
            throw new ScriptProcessingException("It is not possible to add an existing node to the root of a space");
        }
    }
}
