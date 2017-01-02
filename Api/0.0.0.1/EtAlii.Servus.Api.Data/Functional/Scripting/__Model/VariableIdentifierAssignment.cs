namespace EtAlii.Servus.Api.Data
{
    public class VariableIdentifierAssignment : Action
    {
        public string Variable { get; private set; }
        public Identifier Identifier { get; private set; }

        public VariableIdentifierAssignment(Identifier identifier, string variable)
            : base()
        {
            Identifier = identifier;
            Variable = variable;
        }

        internal override void Handle(IHandlerProvider provider, ScriptScope scope, IDataConnection connection)
        {
            Handle<VariableIdentifierAssignmentHandler>(provider, scope, connection);
        }
    }
}
