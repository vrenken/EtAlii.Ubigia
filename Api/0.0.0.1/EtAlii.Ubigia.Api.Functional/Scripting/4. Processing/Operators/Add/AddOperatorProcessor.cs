namespace EtAlii.Ubigia.Api.Functional
{
    internal partial class AddOperatorProcessor : IAddOperatorProcessor
    {
        private readonly IAddOperatorSelector _selector;

        public AddOperatorProcessor(IAddOperatorSelector selector)
        {
            _selector = selector;
        }

        public void Process(OperatorParameters parameters)
        {
            var addOperator = _selector.Select(parameters);
            addOperator.Process(parameters);
        }
    }
}
