namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal class AddOperatorProcessor : IAddOperatorProcessor
    {
        private readonly IAddOperatorSelector _selector;

        public AddOperatorProcessor(IAddOperatorSelector selector)
        {
            _selector = selector;
        }

        public Task Process(OperatorParameters parameters)
        {
            var addOperator = _selector.Select(parameters);
            return addOperator.Process(parameters);
        }
    }
}
