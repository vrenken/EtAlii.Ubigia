namespace EtAlii.Servus.Api.Data
{
    public class VariableOutput : Action
    {
        public string Variable { get; private set; }

        public VariableOutput(string variable)
            : base()
        {
            Variable = variable;
        }

        internal override void Handle(IHandlerFactory factory)
        {
            Handle<VariableOutputHandler>(factory);
        }
    }
}
