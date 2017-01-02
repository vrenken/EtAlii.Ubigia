namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using System;
    using System.CodeDom.Compiler;
    using System.Linq;
    using System.Windows.Shapes;
    using EtAlii.Ubigia.Api.Functional;
    using Xceed.Wpf.DataGrid;

    public class ParseScriptUnitOfworkHandler : UnitOfWorkHandlerBase<ParseScriptUnitOfwork>
    {
        private readonly IDataContext _dataContext;

        private readonly object _lockObject = new object();

        public ParseScriptUnitOfworkHandler(
            IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override void Handle(ParseScriptUnitOfwork unitOfWork)
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
