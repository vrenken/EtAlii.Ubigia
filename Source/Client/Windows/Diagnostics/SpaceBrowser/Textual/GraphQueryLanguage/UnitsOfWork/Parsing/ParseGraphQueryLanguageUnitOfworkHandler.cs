namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Querying;
    using EtAlii.xTechnology.Structure.Workflow;

    public class ParseGraphQueryLanguageUnitOfworkHandler : UnitOfWorkHandlerBase<ParseGraphQueryLanguageUnitOfwork>, IParseGraphQueryLanguageUnitOfworkHandler
    {
        private readonly IGraphQLQueryContext _queryContext;

        public ParseGraphQueryLanguageUnitOfworkHandler(IGraphQLQueryContext queryContext)
        {
            _queryContext = queryContext;
        }

        protected override async void Handle(ParseGraphQueryLanguageUnitOfwork unitOfWork)
        {
            var viewModel = unitOfWork.ScriptViewModel;

            viewModel.Errors = new TextualError[] {};
            var result = await _queryContext.Parse(viewModel.Source);
            viewModel.Query = result.Query;
            viewModel.Errors = result.Errors.Select(error => new TextualError { Text = error.Message, Line = error.Line, Column = error.Column });
            viewModel.CanExecute = !viewModel.Errors.Any() && !string.IsNullOrWhiteSpace(viewModel.Source);
        }
    }
}
