namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Workflow;
    using System;
    using System.CodeDom.Compiler;
    using System.Linq;

    public class CompileCodeUnitOfworkHandler : UnitOfWorkHandlerBase<CompileCodeUnitOfwork>, ICompileCodeUnitOfworkHandler
    {
        private readonly ICodeCompiler _codeCompiler;
        private CompilerResults _compilerResults;
        private readonly ICodeCompilerResultsParser _codeCompilerResultsParser;

        public CompileCodeUnitOfworkHandler(
            ICodeCompiler codeCompiler,
            ICodeCompilerResultsParser codeCompilerResultsParser)
        {
            _codeCompiler = codeCompiler;
            _codeCompilerResultsParser = codeCompilerResultsParser;
        }

        protected override void Handle(CompileCodeUnitOfwork unitOfWork)
        {
            var viewModel = unitOfWork.CodeViewModel;

            _compilerResults = _codeCompiler.Compile(viewModel.Source);
            viewModel.Errors = _codeCompilerResultsParser.Parse(_compilerResults);
            viewModel.CanExecute = !viewModel.Errors.Any() && !String.IsNullOrWhiteSpace(viewModel.Source);
        }

    }
}
