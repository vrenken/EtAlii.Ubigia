namespace EtAlii.Servus.Api.Data
{
    public class VariableUpdateItem : PathAction
    {
        public string UpdateVariable { get; private set; }
        public string Variable { get; private set; }

        public VariableUpdateItem(Path path, string variable, string updateVariable)
            : base(path)
        {
            UpdateVariable = updateVariable;
            Variable = variable;
        }

        internal override void Handle(IHandlerFactory factory)
        {
            Handle<VariableUpdateItemHandler>(factory);
        }
    }
}
