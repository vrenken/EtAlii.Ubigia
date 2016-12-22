namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using System.Reactive.Concurrency;
    using System.Threading.Tasks;

    public class ExecuteCodeUnitOfworkHandler : UnitOfWorkHandlerBase<ExecuteCodeUnitOfwork>
    {
        private readonly IUnitOfWorkProcessor _unitOfWorkProcessor;

        public ExecuteCodeUnitOfworkHandler(
            IUnitOfWorkProcessor unitOfWorkProcessor)
        {
            _unitOfWorkProcessor = unitOfWorkProcessor;
        }

        protected override void Handle(ExecuteCodeUnitOfwork unitOfWork)
        {
            var viewModel = unitOfWork.CodeViewModel;

            _unitOfWorkProcessor.Process(new CompileCodeUnitOfwork(viewModel));

            if (viewModel.CanExecute)
            {
                Task.Factory.StartNew(
                    (o) =>
                    {
                        viewModel.CanExecute = false;
                        viewModel.CanStop = true;
                        //var results = _codeCompilerService.Compile(Code);
                        //Errors = _codeCompilerResultsParser.Parse(results);
                        Task.Delay(2000).Wait();
                        viewModel.CanStop = false;
                        viewModel.CanExecute = true;
                    }, ThreadPoolScheduler.Instance);
            }
        }
    }
}
