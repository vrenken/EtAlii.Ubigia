namespace EtAlii.Servus.Api.Data
{
    public class VariableItemAssignment : PathAction
    {
        public string Variable { get; private set; }

        public VariableItemAssignment(Path path, string variable)
            : base(path)
        {
            Variable = variable;
        }

        internal override void Handle(IHandlerFactory factory)
        {
            Handle<VariableItemAssignmentHandler>(factory);
        }
    }
}
