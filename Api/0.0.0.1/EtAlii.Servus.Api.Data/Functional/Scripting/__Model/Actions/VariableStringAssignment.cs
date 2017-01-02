namespace EtAlii.Servus.Api.Data
{
    public class VariableStringAssignment : Action
    {
        public string Variable { get; private set; }
        public string Text { get; private set; }

        public VariableStringAssignment(string text, string variable)
        {
            Variable = variable;
            Text = text;
        }

        internal override void Handle(IHandlerFactory factory)
        {
            Handle<VariableStringAssignmentHandler>(factory);
        }
    }
}
