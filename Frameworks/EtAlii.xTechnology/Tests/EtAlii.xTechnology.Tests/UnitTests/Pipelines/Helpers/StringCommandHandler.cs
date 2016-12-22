namespace EtAlii.xTechnology.Tests
{
    using EtAlii.xTechnology.Structure;

    internal class StringCommandHandler : ICommandHandler<StringCommand, string>
    {
        public StringCommandHandler()
        {
        }

        public StringCommand Create(string parameter)
        {
            return new StringCommand(parameter);
        }

        public void Handle(StringCommand command)
        {
        }
    }
}