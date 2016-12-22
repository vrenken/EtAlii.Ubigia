
namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.Workflow;

    public class ProcessEntryCommandHandler : CommandHandlerBase<ProcessEntryCommand>
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
