namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Structure.Workflow;

    public class RetrieveEntryCommandHandler : CommandHandlerBase<RetrieveEntryCommand>, IRetrieveEntryCommandHandler
    {
        private readonly IFabricContext _fabric;
        private readonly IGraphContext _graphContext;

        public RetrieveEntryCommandHandler(
            IFabricContext fabric,
            IGraphContext graphContext)
        {
            _fabric = fabric;
            _graphContext = graphContext;
        }

        protected override void Handle(RetrieveEntryCommand command)
        {
            IReadOnlyEntry entry = null;
            var task = Task.Run(async () =>
            {
                entry = await _fabric.Entries.Get(command.Identifier, new ExecutionScope(false));
            });
            task.Wait();

            _graphContext.CommandProcessor.Process(new ProcessEntryCommand(entry, command.ProcessReason), _graphContext.ProcessEntryCommandHandler);
        }
    }
}
