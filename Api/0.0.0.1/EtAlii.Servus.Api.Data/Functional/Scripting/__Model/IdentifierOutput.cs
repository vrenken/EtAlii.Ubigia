namespace EtAlii.Servus.Api.Data
{
    public class IdentifierOutput : Action
    {
        public Identifier Identifier { get; private set; }

        public IdentifierOutput(Identifier identifier)
            : base()
        {
            Identifier = identifier;
        }

        internal override void Handle(IHandlerProvider provider, ScriptScope scope, IDataConnection connection)
        {
            Handle<IdentifierOutputHandler>(provider, scope, connection);
        }
    }
}
