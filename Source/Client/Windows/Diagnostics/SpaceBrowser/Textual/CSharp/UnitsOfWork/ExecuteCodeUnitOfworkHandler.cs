namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Structure.Workflow;

    public class ExecuteCodeUnitOfworkHandler : UnitOfWorkHandlerBase<ExecuteCodeUnitOfwork>, IExecuteCodeUnitOfworkHandler
    {
        private readonly IGraphContext _graphContext;
        private readonly ICompileCodeUnitOfworkHandler _compileCodeUnitOfworkHandler;

        public ExecuteCodeUnitOfworkHandler(
            ICompileCodeUnitOfworkHandler compileCodeUnitOfworkHandler, 
            IGraphContext graphContext)
        {
            _compileCodeUnitOfworkHandler = compileCodeUnitOfworkHandler;
            _graphContext = graphContext;
        }

        protected override void Handle(ExecuteCodeUnitOfwork unitOfWork)
        {
            var viewModel = unitOfWork.CodeViewModel;

            _graphContext.UnitOfWorkProcessor.Process(new CompileCodeUnitOfwork(viewModel), _compileCodeUnitOfworkHandler);

            if (viewModel.CanExecute)
            {
                Task.Factory.StartNew(() =>
                    {
                        viewModel.CanExecute = false;
                        viewModel.CanStop = true;
                        //var results = _codeCompilerService.Compile(Code)
                        //Errors = _codeCompilerResultsParser.Parse(results)
                        Task.Delay(2000).Wait();
                        viewModel.CanStop = false;
                        viewModel.CanExecute = true;
                    });
                //ThreadPoolScheduler.Instance
            }
        }
    }
}
