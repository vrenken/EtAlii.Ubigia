namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using System;
    using System.CodeDom.Compiler;
    using System.Linq;

    public class CompileCodeUnitOfworkHandler : UnitOfWorkHandlerBase<CompileCodeUnitOfwork>
    {
        private readonly CodeCompiler _codeCompiler;
        private CompilerResults _compilerResults;
        private readonly CodeCompilerResultsParser _codeCompilerResultsParser;

        private readonly object _lockObject = new object();

        public CompileCodeUnitOfworkHandler(
            CodeCompiler codeCompiler,
            CodeCompilerResultsParser codeCompilerResultsParser)
        {
            _codeCompiler = codeCompiler;
            _codeCompilerResultsParser = codeCompilerResultsParser;
        }

        protected override void Handle(CompileCodeUnitOfwork unitOfWork)
        {
            var viewModel = unitOfWork.CodeViewModel;

            _compilerResults = _codeCompiler.Compile(viewModel.Code);
            viewModel.Errors = _codeCompilerResultsParser.Parse(_compilerResults);
            viewModel.CanExecute = !viewModel.Errors.Any() && !String.IsNullOrWhiteSpace(viewModel.Code);
        }

    }
}
