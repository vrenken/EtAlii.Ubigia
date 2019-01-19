namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    internal class RemoveOperatorProcessor : IRemoveOperatorProcessor
    {
        private readonly IRemoveOperatorSelector _selector;

        public RemoveOperatorProcessor(IRemoveOperatorSelector selector)
        {
            _selector = selector;
        }

        public async Task Process(OperatorParameters parameters)
        {
            var removeOperator = _selector.Select(parameters);
            await removeOperator.Process(parameters);
        }
    }
}
