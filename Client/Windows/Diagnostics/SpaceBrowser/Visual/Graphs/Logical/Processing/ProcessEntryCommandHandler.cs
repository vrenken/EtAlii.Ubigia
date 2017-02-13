﻿namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;

    public class ProcessEntryCommandHandler : CommandHandlerBase<ProcessEntryCommand>, IProcessEntryCommandHandler
    {
        private readonly IFabricContext _fabric;
        private readonly IGraphContext _graphContext;
        private readonly IDocumentViewModelProvider _documentViewModelProvider;

        public ProcessEntryCommandHandler(
            IFabricContext fabric,
            IGraphContext graphContext,
            IDocumentViewModelProvider documentViewModelProvider)
        {
            _fabric = fabric;
            _graphContext = graphContext;
            _documentViewModelProvider = documentViewModelProvider;
        }

        protected override void Handle(ProcessEntryCommand command)
        {
            var graphDocumentViewModel = _documentViewModelProvider.GetInstance<IGraphDocumentViewModel>();
            var addEntryToGraphCommand = new AddEntryToGraphCommand(command.Entry, command.ProcessReason, graphDocumentViewModel);
            _graphContext.CommandProcessor.Process(addEntryToGraphCommand, _graphContext.AddEntryToGraphCommandHandler);
        }
    }
}
