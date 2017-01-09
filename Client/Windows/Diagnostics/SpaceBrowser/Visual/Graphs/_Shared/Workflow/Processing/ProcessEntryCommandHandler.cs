
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Workflow;

    public class ProcessEntryCommandHandler : CommandHandlerBase<ProcessEntryCommand>, IProcessEntryCommandHandler
    {
        private readonly IFabricContext _fabric;
        private readonly ICommandProcessor _commandProcessor;

        public ProcessEntryCommandHandler(
            IFabricContext fabric,
            ICommandProcessor commandProcessor)
        {
            _fabric = fabric;
            _commandProcessor = commandProcessor;
        }

        protected override void Handle(ProcessEntryCommand command)
        {
            _commandProcessor.Process(new AddEntryToGraphCommand(command.Entry, command.ProcessReason));
        }
    }
}
