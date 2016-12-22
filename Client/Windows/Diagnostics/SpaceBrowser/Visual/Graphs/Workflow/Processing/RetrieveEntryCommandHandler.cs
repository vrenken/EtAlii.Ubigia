
namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.Workflow;

    public class RetrieveEntryCommandHandler : CommandHandlerBase<RetrieveEntryCommand>
    {
        private readonly IFabricContext _fabric;
        private readonly ICommandProcessor _commandProcessor;

        public RetrieveEntryCommandHandler(
            IFabricContext fabric,
            ICommandProcessor commandProcessor)
        {
            _fabric = fabric;
            _commandProcessor = commandProcessor;
        }

        protected override void Handle(RetrieveEntryCommand command)
        {
            IReadOnlyEntry entry = null;
            var task = Task.Run(async () =>
            {
                entry = await _fabric.Entries.Get(command.Identifier, new ExecutionScope(false));
            });
            task.Wait();

            _commandProcessor.Process(new ProcessEntryCommand(entry, command.ProcessReason));
        }
    }
}
