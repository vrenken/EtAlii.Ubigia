namespace EtAlii.Ubigia.Api.Functional
{
    internal class RemoveOperatorProcessor : IRemoveOperatorProcessor
    {
        private readonly IRemoveOperatorSelector _selector;

        public RemoveOperatorProcessor(IRemoveOperatorSelector selector)
        {
            _selector = selector;
        }

        public void Process(OperatorParameters parameters)
        {
            var removeOperator = _selector.Select(parameters);
            removeOperator.Process(parameters);
        }
    }
}
