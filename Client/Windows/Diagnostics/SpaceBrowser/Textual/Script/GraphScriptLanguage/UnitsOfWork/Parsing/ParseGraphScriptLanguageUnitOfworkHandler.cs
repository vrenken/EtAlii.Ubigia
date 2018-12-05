namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Workflow;
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional;

    public class ParseGraphScriptLanguageUnitOfworkHandler : UnitOfWorkHandlerBase<ParseGraphScriptLanguageUnitOfwork>, IParseGraphScriptLanguageUnitOfworkHandler
    {
        private readonly IDataContext _dataContext;

//        private readonly object _lockObject = new object();

        public ParseGraphScriptLanguageUnitOfworkHandler(
            IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override void Handle(ParseGraphScriptLanguageUnitOfwork unitOfWork)
        {
            var viewModel = unitOfWork.ScriptViewModel;

                viewModel.Errors = new TextualError[] {};
                var result = _dataContext.Scripts.Parse(viewModel.Code);
                viewModel.Script = result.Script;
                viewModel.Errors = result.Errors.Select(error => new TextualError { Text = error.Message, Line = error.Line, Column = error.Column });
            viewModel.CanExecute = !viewModel.Errors.Any() && !String.IsNullOrWhiteSpace(viewModel.Code);
        }

    }
}
