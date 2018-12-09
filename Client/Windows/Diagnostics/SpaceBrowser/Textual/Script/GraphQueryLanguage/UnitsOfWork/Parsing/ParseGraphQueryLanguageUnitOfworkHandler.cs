namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Workflow;
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Querying;

    public class ParseGraphQueryLanguageUnitOfworkHandler : UnitOfWorkHandlerBase<ParseGraphQueryLanguageUnitOfwork>, IParseGraphQueryLanguageUnitOfworkHandler
    {
        private readonly IDataContext _dataContext;

        public ParseGraphQueryLanguageUnitOfworkHandler(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override void Handle(ParseGraphQueryLanguageUnitOfwork unitOfWork)
        {
            var viewModel = unitOfWork.ScriptViewModel;

            viewModel.Errors = new TextualError[] {};
            
            var queryContext = _dataContext.CreateGraphQLQueryContext();
            //var result = _dataContext.Queries.Parse(viewModel.Code);
            //viewModel.Query = result.Query;
            //viewModel.Errors = result.Errors.Select(error => new TextualError { Text = error.Message, Line = error.Line, Column = error.Column });
            viewModel.CanExecute = !viewModel.Errors.Any() && !String.IsNullOrWhiteSpace(viewModel.Code);
        }
    }
}
