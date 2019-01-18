namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    internal class AddOperatorProcessor : IAddOperatorProcessor
    {
        private readonly IAddOperatorSelector _selector;

        public AddOperatorProcessor(IAddOperatorSelector selector)
        {
            _selector = selector;
        }

        public async Task Process(OperatorParameters parameters)
        {
            var addOperator = _selector.Select(parameters);
            await addOperator.Process(parameters);
        }
    }
}
