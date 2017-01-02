namespace EtAlii.Servus.Api.Data
{
    public class VariableItemsAssignment : PathAction
    {
        public string Variable { get; private set; }

        public VariableItemsAssignment(Path path, string variable)
            : base(path)
        {
            Variable = variable;
        }

        internal override void Handle(IHandlerFactory factory)
        {
            Handle<VariableItemsAssignmentHandler>(factory);
        }
    }
}
