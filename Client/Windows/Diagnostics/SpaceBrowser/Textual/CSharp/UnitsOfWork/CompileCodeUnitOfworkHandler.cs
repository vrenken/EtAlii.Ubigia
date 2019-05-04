﻿namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using EtAlii.xTechnology.Workflow;

    public class CompileCodeUnitOfworkHandler : UnitOfWorkHandlerBase<CompileCodeUnitOfwork>, ICompileCodeUnitOfworkHandler
    {
        private readonly ICodeCompiler _codeCompiler;
        private CompilerResults _compilerResults;
        private readonly ICodeCompilerResultsParser _codeCompilerResultsParser;

//        private readonly object _lockObject = new object()

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
            viewModel.CanExecute = !viewModel.Errors.Any() && !string.IsNullOrWhiteSpace(viewModel.Source);
        }

    }
}
